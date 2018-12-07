using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class PlayerScore : MonoBehaviour
{

    private float timeLeft = 120;
    public int score = 0;
    public GameObject timeLeftUI;
    public GameObject scoreUI;

    void Start()
    {
        DataManagement.dataManagement.LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timeLeftUI.gameObject.GetComponent<Text>().text = ("NB: Defeat enemy with attack or by jumping on their head"+ "\nAttack: left mouse/ctrl" + "\nJump: space" + "\nMove: dir arrows");
        scoreUI.gameObject.GetComponent<Text>().text = ("Time Left: " + (int)timeLeft + "\n" + "\nScore: " + score + "\n" + "\nHigh Score: " + DataManagement.dataManagement.highScore);

        //Debug.Log(timeLeft);
        if (timeLeft < 0.1f)
        {
            SceneManager.LoadScene("Level_1");
        }
    }

    void OnTriggerEnter2D(Collider2D trig)
    {

        if (trig.gameObject.name == "EndLevel")
        {
            Debug.Log("Endlevel");
            CountFinalScore();
            //DisplayDialog("Congratulations", "Level Passed", "Ok"); 
        }
        else if (trig.gameObject.tag == "Jewel")
        {
            score += 10;
            Destroy(trig.gameObject);
        }
        else if (trig.gameObject.tag == "Chest")
        {
            score += 50;
            Destroy(trig.gameObject);
        }
    }

    void CountFinalScore()
    {
        Debug.Log("Data says high score is currently: " + DataManagement.dataManagement.highScore);
        score = score + ((int)timeLeft * 10);
        DataManagement.dataManagement.highScore = score + ((int)timeLeft * 10);
        DataManagement.dataManagement.SaveData();
        Debug.Log("Now weve added the score to DataManagement, Data says high score is currently:" + DataManagement.dataManagement.highScore);
    }
}
