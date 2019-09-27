using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private Canvas levelSelectMenu;
    public SaveController saveC;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject resetMenu;

    //test variables
    public int levelSelector = 0; 

    private void Awake()
    {
        saveC = FindObjectOfType<SaveController>();
        //levelSelectMenu = GameObject.FindGameObjectWithTag("pauseMenu").GetComponent<Canvas>();
        //Debug.Log(levelSelectMenu.gameObject.name);
        resetMenu = GameObject.Find("ResetGameMenu").gameObject;
        resetMenu.SetActive(false);
        optionsMenu = GameObject.Find("Options").gameObject;
        optionsMenu.SetActive(false);
        pauseMenu = GameObject.Find("PauseMenu").gameObject;
        pauseMenu.SetActive(false);
        

    }

    void Start()
    {
        Button[] levelbuttons = levelSelectMenu.transform.GetChild(0).GetComponentsInChildren<Button>();
        foreach (Button button in levelbuttons)
        {
            button.interactable = false;
        }
        UpdatePauseMenu();
    }

    public void GoToLevel(int level)
    {
        //SceneManager.LoadScene("Level"+level);
        SceneManager.LoadScene("Prototype");
    }

    public void UpdatePauseMenu()
    {
        Button[] levelbuttons = levelSelectMenu.transform.GetChild(0).GetComponentsInChildren<Button>();

        for (int i=0; i < saveC.save.maxReachedLevel; i++)
        {
            levelbuttons[i].interactable = true;
            Debug.Log(saveC.save.maxReachedLevel);
        }
    }

    public void GoToLevelSelectScene()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void ResetLevel()
    {
        //TODO
    }

    public void SwitchLanguageToDanish()
    {

    }

    public void SwitchLanguageToEnglish()
    {

    }

    public void MusicChangeToLow()
    {

    }

    public void MusicChangeToHigh()
    {

    }

    public void MusicChangeToOff()
    {

    }

    public void SoundTurnOff()
    {

    }

    public void SoundTurnOn()
    {

    }

    public void GoToRestartMenu()
    {

        resetMenu.SetActive(true);
    }

    public void BackToGame()
    {
        pauseMenu.SetActive(false);

        optionsMenu.SetActive(false);


        resetMenu.SetActive(false);
    }

    public void GoToPauseMenu()
    {
        pauseMenu.SetActive(true);

        optionsMenu.SetActive(false);
    }

    public void GoToOptionsMenu()
    {
        optionsMenu.SetActive(true);


        resetMenu.SetActive(false);
    }

    public void RestartGameProgress()
    {
        Debug.Log("Game Progress is restarted");
    }

}
