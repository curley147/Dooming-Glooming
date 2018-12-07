using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour {

    public static int enemyHealth = 100;
    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -20)
        {
            Debug.Log("Enemy dead");
            Destroy(gameObject);
        }
    }

}
