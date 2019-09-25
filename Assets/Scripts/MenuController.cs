using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    private Canvas pauseMenu;
    public bool isPaused=false;
    //public LevelController lvlC;
    public SaveController saveC;

    private void Awake()
    {
        //lvlC = FindObjectOfType<LevelController>();
        saveC = FindObjectOfType<SaveController>();
        pauseMenu = GameObject.FindGameObjectWithTag("pauseMenu").GetComponent<Canvas>();
        Debug.Log(pauseMenu.gameObject.name);
    }

    void Start()
    {
        Button[] levelbuttons = pauseMenu.transform.GetChild(0).GetComponentsInChildren<Button>();

        foreach (Button button in levelbuttons)
        {
            button.interactable = false;
        }
        UpdatePauseMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePauseMenu()
    {


        if (isPaused)
        {
            isPaused = false;

            Time.timeScale = 1;
            pauseMenu.enabled= false;
            Debug.Log("game resumed");
        }
        else
        {
            UpdatePauseMenu();
            isPaused = true;

            Time.timeScale = 0;
            pauseMenu.enabled = true ;

            Debug.Log("game paused");
        }

    }

    public void ExitPauseMenu()
    {

        
    }

    public void GoToLevel(int level)
    {
        SceneManager.LoadScene("Level"+level);
    }

    public void UpdatePauseMenu()
    {

        Button[] levelbuttons = pauseMenu.transform.GetChild(0).GetComponentsInChildren<Button>();

        for (int i=0; i < saveC.save.maxReachedLevel; i++)
        {
            levelbuttons[i].interactable = true;
            Debug.Log(saveC.save.maxReachedLevel);
        }
    }

    public void ChangeScene(string scene)
    {

    }

    public void ChangeSceneToLevelSelect()
    {
        SceneManager.LoadScene(1);
    }


}
