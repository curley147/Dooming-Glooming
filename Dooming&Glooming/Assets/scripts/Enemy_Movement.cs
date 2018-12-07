using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{

    public int enemySpeed = 5;
    public int xMoveDirection = -1;
    public bool facingEast;
    public float dist = 1f;

    // Use this for initialization
    /*void Start () {
        GetComponent<SpriteRenderer>().flipX = true;
    }*/

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody2D>().tag == "BigEnemy")
        {
            dist = 3.2f;
            enemySpeed = 10;
        }
        //creates rays from centre of object in direction odf movement
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(xMoveDirection, 0));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMoveDirection, 0) * enemySpeed;
        //Debug.Log("hit = " + hit
        //the raycast hits its own enemy so we must include it in condition
        if (hit.distance != null && hit.collider != null && hit.distance < dist && hit.collider.tag != "Enemy" && hit.collider.tag != "BigEnemy")
        {
            //GetComponent<SpriteRenderer>().flipX = false;
            ReverseEnemy();
            if (hit.collider.tag == "Player")
            {
                Destroy(hit.collider.gameObject);
            }
        }
        //enemy avoids jewels
        else if (hit.distance != null && hit.collider != null && hit.distance < dist && hit.collider.tag != "Jewel")
        {
            //disable jewels collider
            hit.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        
    }

    void ReverseEnemy()
    {
        if (xMoveDirection > 0)
        {
            xMoveDirection = -1;
        }
        else
        {
            xMoveDirection = 1;
        }
        facingEast = !facingEast;
        //creating vector variable to store localScale value temporarily
        Vector2 localScale = gameObject.transform.localScale;
        //times by -1 to reverse direction
        localScale.x *= -1;
        //store new value in variable
        transform.localScale = localScale;
      
    }
}
