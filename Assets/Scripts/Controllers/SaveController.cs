using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveController : MonoBehaviour
{

    public Save save;
    private LevelController lvlC;

    // Start is called before the first frame update

    void Awake()
    {
        lvlC = FindObjectOfType<LevelController>();

        LoadGame();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateSaveGameObject()
    {
        for(int i =1; i<GlobalVariables.totalNumberOfLevels; i++)
        {
            save.SaveScore(i,lvlC.currentLevelScores[i]);
        }

        save.maxReachedLevel = lvlC.maxReachedLevel;

    }

    public void SaveGame()
    {
        // 1
        CreateSaveGameObject();
        // 2
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        // 3
       
        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        // 1
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {

            // 2
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            save = (Save)bf.Deserialize(file);
            file.Close();

            // 3
            
            //lvlC.currentLevelScores = save.levelScores;
            //lvlC.maxReachedLevel = save.maxReachedLevel;
            
            

            Debug.Log("Game Loaded");
        }
        else
        {
            save = new Save();
            Debug.Log("No game saved!, Creating clean save");
        }
    }
}
