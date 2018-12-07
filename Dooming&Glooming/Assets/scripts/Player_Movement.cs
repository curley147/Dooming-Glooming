using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    public int playerSpeed = 10;
    public int jumpPower = 1250;
    private float moveX;
    public bool onGround;
    public float spriteOffsetX = 1.6f;
    public float spriteOffsetY = 3.5f;

    //void Start()
    //{
    //}

        // Update is called once per frame
        void Update()
    {
        PlayerMove();
        PlayerRaycast();
    }

    void PlayerMove()
    {
        //Controls
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && onGround == true)
        {
            Jump();
        }
        else if (Input.GetButtonDown("Fire1") && onGround == true)
        {
           // Debug.Log("a button pressed");
            StartCoroutine("attack");
            
        }

        //Animation
        if (moveX != 0)
        {
            GetComponent<Animator>().SetBool("IsWalking", true);
        }
        else {
            GetComponent<Animator>().SetBool("IsWalking", false);
        }
        
        //Player Direction
        if (moveX < 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveX > 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        //Physics
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }
    IEnumerator attack()
    {
        GetComponent<Animator>().SetBool("IsFighting", true);
        yield return new WaitForSeconds(1);
        RaycastHit2D rayLeft = Physics2D.Raycast(transform.position, Vector2.left);
        RaycastHit2D rayRight = Physics2D.Raycast(transform.position, Vector2.right);
        if (rayLeft.distance != null && rayLeft.collider != null && rayLeft.distance < spriteOffsetY && rayLeft.collider.tag == "Enemy")
        {
            rayLeft.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 8;
            rayLeft.collider.gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
            rayLeft.collider.gameObject.GetComponent<Rigidbody2D>().transform.Rotate(Vector3.forward * -90);
            rayLeft.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            rayLeft.collider.gameObject.GetComponent<Enemy_Movement>().enabled = false;
        } 
        else if (rayRight.distance != null && rayRight.collider != null && rayRight.distance < spriteOffsetY && (rayRight.collider.tag == "Enemy" || rayRight.collider.tag == "BigEnemy"))
        {
            rayRight.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 8;
            rayRight.collider.gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
            rayRight.collider.gameObject.GetComponent<Rigidbody2D>().transform.Rotate(Vector3.forward * -90);
            rayRight.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            rayRight.collider.gameObject.GetComponent<Enemy_Movement>().enabled = false;
        }
        GetComponent<Animator>().SetBool("IsFighting", false);

    }
    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower);
        onGround = false;
    }

    // method used before Raycasting
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Player has collided wid " + col.collider.name);

        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Platform")
        {
            onGround = true;
        }
        else if (col.gameObject.tag == "Chest")
        {
            Destroy(col.gameObject);
        }
    }

    void PlayerRaycast()
    {
        //Ray up
        RaycastHit2D rayRight = Physics2D.Raycast(transform.position, Vector2.right);
        if (rayRight.distance != null && rayRight.collider != null && rayRight.distance < spriteOffsetX && rayRight.collider.tag == "Chest")
        {
            Debug.Log("box hit");
            //Destroy(rayRight.collider.gameObject);
        }
        
        //Ray down
        RaycastHit2D rayDown = Physics2D.Raycast(transform.position, Vector2.down);
        if (rayDown.distance != null && rayDown.collider != null && rayDown.distance < spriteOffsetY && (rayDown.collider.tag == "Enemy" || rayDown.collider.tag == "BigEnemy"))
        {
            Debug.Log("landed on Enemy");
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000);
            rayDown.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 200);
            rayDown.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 8;
            rayDown.collider.gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
            rayDown.collider.gameObject.GetComponent<Rigidbody2D>().transform.Rotate(Vector3.forward * -90);
            rayDown.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            rayDown.collider.gameObject.GetComponent<Enemy_Movement>().enabled = false;

            //Destroy(rayDown.collider.gameObject);
        } 
    }
}
