using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManagement : MonoBehaviour {

    public static DataManagement dataManagement;

    public int highScore;

    void Awake()
    {
        //overwriting functionality
        if (dataManagement == null)
        {
            DontDestroyOnLoad(gameObject);
            dataManagement = this;
        }
        else if (dataManagement != this)
        {
            Destroy(gameObject);
        }
    }

    public void SaveData()
    {
        // instantiating bin formatter
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        // create file to save data to
        FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.dat");
        // creates container for data
        GameData gameData = new GameData();
        // setting this objects high score to GameData's high score
        gameData.highscore = highScore;
        // serialises/encrypts data
        binaryFormatter.Serialize(file, gameData);
        // closes file
        file.Close();
    }

    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/gameInfo.dat"))
        {
            // instantiating bin formatter
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            //opens file
            FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);
            // decrypt data
            GameData gameData = (GameData)binaryFormatter.Deserialize(file);
            // close file
            file.Close();
            highScore = gameData.highscore;
        }
    }
}

[Serializable]
class GameData {
    public int highscore;
}