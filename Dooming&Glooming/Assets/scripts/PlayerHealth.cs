using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public static int playerHealth = 100;
    public bool isDead;

    // Use this for initialization
    void Start () {
      isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -15)
        {
            Debug.Log("Player dead");
            isDead = true;
            Die();
            
        }
    }

    void Die()
    {
        SceneManager.LoadScene("Level_1");
    }

    //IEnumerator Die() {
    //  yield return new WaitForSeconds(3); 
    //SceneManager.LoadScene("Draft_1");

    //}
}
