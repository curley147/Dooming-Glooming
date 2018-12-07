using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{

    private GameObject player;

    //variables to store coordinates of min/max values of camera position
    public float xMin = -25.8f;
    public float xMax = 9.35f;
    public float yMin = -0.8f;
    public float yMax = 0.8f;
    // Use this for initialization
    void Start()
    {
        //storing the player (unity) in variable using tag
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // LateUpdate is called at the end of Update cycle
    void LateUpdate()
    {
        //setting boundary of camera
        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);

    }
}
