using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private BallController ballController;
    private Canvas levelSelectMenu;
    private SaveController saveC;
    private GameObject pauseMenu;
    private GameObject optionsMenu;
    private GameObject resetMenu;
    [HideInInspector]
    public string language;
    private string musicVolume;
    private string soundOnOff;

    //test variables
    //public int levelSelector = 0;


    #region menuvariables
    private GameObject settingsTextImage;
    private GameObject indstillingerTextImage;

    private Text[] soundOnOffContainer;
    private Text[] musicContainer;

    private GameObject englishButton;
    private GameObject danishButton;
    private GameObject restartButton;

    private GameObject musicHigh;
    private GameObject musicLow;
    private GameObject musicOff;

    private GameObject soundOn;
    private GameObject soundOff;

    private GameObject restartMenuDanish;
    private GameObject restartMenuEnglish;

    private GameObject optionsButtonDanish;
    private GameObject optionsButtonEnglish;

    #endregion

    [HideInInspector]
    public UnityEvent ChangeLanguageEvent;

    private void Awake()
    {
        RefferenceMenuVariables();
        MenuUpdate(); //setup menu
        saveC = FindObjectOfType<SaveController>();
        //levelSelectMenu = GameObject.FindGameObjectWithTag("pauseMenu").GetComponent<Canvas>();
        //Debug.Log(levelSelectMenu.gameObject.name);
        resetMenu = GameObject.Find("ResetGameContainer").gameObject;
        resetMenu.SetActive(false);
        optionsMenu = GameObject.Find("Options").gameObject;
        optionsMenu.SetActive(false);
        pauseMenu = GameObject.Find("PauseMenu").gameObject;
        pauseMenu.SetActive(false);

        ballController = FindObjectOfType<BallController>();
    }

    void Start()
    {
        if (ChangeLanguageEvent == null)
            ChangeLanguageEvent = new UnityEvent();

        if (levelSelectMenu == null)
            return;

        //Button[] levelbuttons = levelSelectMenu.transform.GetChild(0).GetComponentsInChildren<Button>();
        //foreach (Button button in levelbuttons)
        //{
        //    button.interactable = false;
        //}

        //UpdatePauseMenu();
    }

    public void GoToLevel(int level)
    {
        AkSoundEngine.PostEvent("PressButton_event", gameObject);
        SceneManager.LoadScene("Level" + level);
        //SceneManager.LoadScene("Prototype");
    }

    public void UpdatePauseMenu()
    {
        Button[] levelbuttons = levelSelectMenu.transform.GetChild(0).GetComponentsInChildren<Button>();

        for (int i = 0; i < saveC.save.maxReachedLevel; i++)
        {
            levelbuttons[i].interactable = true;
            Debug.Log(saveC.save.maxReachedLevel);
        }
    }

    public void GoToLevelSelectScene()
    {
        AkSoundEngine.PostEvent("PressButton_event", gameObject);
        SceneManager.LoadScene("Level Select");
    }

    public void ResetLevel()
    {
        //TODO
    }

    public void SwitchLanguageToDanish()
    {
        AkSoundEngine.PostEvent("PressButton_event", gameObject);
        PlayerPrefs.SetString("Lang", "Danish");
        Debug.Log("Changed Lang to danish");

        MenuUpdate();

        ChangeLanguageEvent?.Invoke();
    }

    public void SwitchLanguageToEnglish()
    {
        AkSoundEngine.PostEvent("PressButton_event", gameObject);
        PlayerPrefs.SetString("Lang", "English");

        Debug.Log("Changed Lang to English");

        MenuUpdate();

        ChangeLanguageEvent?.Invoke();
    }

    public void MusicChangeToLow()
    {
        AkSoundEngine.PostEvent("PressButton_event", gameObject);
        PlayerPrefs.SetString("MusVol", "Low");
        MenuUpdate();
    }

    public void MusicChangeToHigh()
    {
        AkSoundEngine.PostEvent("PressButton_event", gameObject);
        PlayerPrefs.SetString("MusVol", "High");
        MenuUpdate();
    }

    public void MusicChangeToOff()
    {
        AkSoundEngine.PostEvent("PressButton_event", gameObject);
        PlayerPrefs.SetString("MusVol", "Off");
        MenuUpdate();
    }

    public void SoundTurnOff()
    {
        AkSoundEngine.PostEvent("PressButton_event", gameObject);
        PlayerPrefs.SetString("SoundOnOff", "Off");
        MenuUpdate();
    }

    public void SoundTurnOn()
    {
        AkSoundEngine.PostEvent("PressButton_event", gameObject);
        PlayerPrefs.SetString("SoundOnOff", "On");
        MenuUpdate();
    }

    public void GoToRestartMenu()
    {
        AkSoundEngine.PostEvent("PressButton_event", gameObject);
        resetMenu.SetActive(true);
        Time.timeScale = ballController.TimeScale;
    }

    public void BackToGame()
    {
        pauseMenu.SetActive(false);

        optionsMenu.SetActive(false);


        resetMenu.SetActive(false);
        Time.timeScale = ballController.TimeScale;

        AkSoundEngine.PostEvent("menuResume_event", gameObject);
    }

    public void GoToPauseMenu()
    {
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
        Time.timeScale = 0;

        AkSoundEngine.PostEvent("menuPause_event", gameObject);
    }

    public void GoToOptionsMenu()
    {
        AkSoundEngine.PostEvent("PressButton_event", gameObject);
        optionsMenu.SetActive(true);
        resetMenu.SetActive(false);
    }

    public void RestartGameProgress()
    {
        AkSoundEngine.PostEvent("PressButton_event", gameObject);
        Debug.Log("Game Progress is restarted");
        // todo merge devil deal save branch into Develop to get restart code
        GoToLevelSelectScene();
    }

    public void UpdateLanguageTextToDanish()
    {

    }

    public void UpdateLanguageTextToEnglísh()
    {

    }

    private void MenuUpdate()
    {
        //setup all the playerprefs the first time the player plays

        if (!PlayerPrefs.HasKey("Lang"))
        {
            PlayerPrefs.SetString("Lang", "English");
        }

        if (!PlayerPrefs.HasKey("MusVol"))
        {
            PlayerPrefs.SetString("MusVol", "High");
        }

        if (!PlayerPrefs.HasKey("SoundOnOff"))
        {
            PlayerPrefs.SetString("SoundOnOff", "On");
        }

        //Show correct information in menu

        language = PlayerPrefs.GetString("Lang");
        musicVolume = PlayerPrefs.GetString("MusVol");
        soundOnOff = PlayerPrefs.GetString("SoundOnOff");

        //||set language text---------------------------------------------------------

        //gameobject variables

        

        if (language == "English")
        {
            settingsTextImage.SetActive(true);
            indstillingerTextImage.SetActive(false);

            foreach (Text text in soundOnOffContainer)
            {
                text.text = "Sound";
            }

            foreach (Text text in musicContainer)
            {
                text.text = "Volume";
            }

            englishButton.SetActive(true);
            danishButton.SetActive(false);

            restartButton.GetComponentInChildren<Text>().text = "Reset Game";

            restartMenuEnglish.SetActive(true);
            restartMenuDanish.SetActive(false);

            optionsButtonEnglish.SetActive(true);
            optionsButtonDanish.SetActive(false);

            AkSoundEngine.SetState("dia_lang", "EN");

        }
        else if (language == "Danish")
        {
            settingsTextImage.SetActive(false);
            indstillingerTextImage.SetActive(true);

            foreach (Text text in soundOnOffContainer)
            {
                text.text = "Lyd";
            }

            foreach (Text text in musicContainer)
            {
                text.text = "Lydstyrke";
            }

            englishButton.SetActive(false);
            danishButton.SetActive(true);

            restartButton.GetComponentInChildren<Text>().text = "Slet Fremskridt";

            restartMenuEnglish.SetActive(false);
            restartMenuDanish.SetActive(true);


            optionsButtonEnglish.SetActive(false);
            optionsButtonDanish.SetActive(true);

            AkSoundEngine.SetState("dia_lang", "DA");
        }
        else
        {
            Debug.Log("language in menucontroller has incorrect value: "+ language);
        }

        //Volume
        if (musicVolume == "High")
        {
            musicHigh.SetActive(true);
            musicLow.SetActive(false);
            musicOff.SetActive(false);
            AkSoundEngine.SetState("MusicVol", "High");
        }
        else if (musicVolume == "Low")
        {
            musicHigh.SetActive(false);
            musicLow.SetActive(true);
            musicOff.SetActive(false);
            AkSoundEngine.SetState("MusicVol", "Low");
        }
        else if (musicVolume == "Off")
        {
            musicHigh.SetActive(false);
            musicLow.SetActive(false);
            musicOff.SetActive(true);
            AkSoundEngine.SetState("MusicVol", "Off");
        }


        //SoundOnOff
        if (soundOnOff == "On")
        {
            soundOn.SetActive(true);
            soundOff.SetActive(false);
            AkSoundEngine.SetState("SoundVol", "On");

        }
        else if (soundOnOff == "Off")
        {
            soundOn.SetActive(false);
            soundOff.SetActive(true);
            AkSoundEngine.SetState("SoundVol", "Off");
        }
        



    }

    public void RefferenceMenuVariables()
    {
        settingsTextImage = GameObject.Find("Settings text image");
        indstillingerTextImage = GameObject.Find("Indstillinger text image");

        soundOnOffContainer = GameObject.Find("SoundOnOffContainer").gameObject.GetComponentsInChildren<Text>();
        musicContainer = GameObject.Find("MusicSettingscontainer").gameObject.GetComponentsInChildren<Text>();

        musicHigh = GameObject.Find("MusicHighButton").gameObject;
        musicLow = GameObject.Find("MusicLowButton").gameObject;
        musicOff = GameObject.Find("MusicOffButton").gameObject;

        soundOn = GameObject.Find("SoundOnButton").gameObject;
        soundOff = GameObject.Find("SoundOffButton").gameObject;


        englishButton = GameObject.Find("LanguageEnglishButton").gameObject;
        danishButton = GameObject.Find("LanguageDanishButton").gameObject;
        restartButton = GameObject.Find("ResetGameButton").gameObject;

        restartMenuEnglish = GameObject.Find("ResetGameMenuEnglish").gameObject;
        restartMenuDanish = GameObject.Find("ResetGameMenuDanish").gameObject;

        optionsButtonDanish = GameObject.Find("OptionsButtonDanish").gameObject;
        optionsButtonEnglish = GameObject.Find("OptionsButtonEnglish").gameObject;

    }

}


