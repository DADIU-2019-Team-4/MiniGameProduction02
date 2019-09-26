using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private Canvas pauseMenu;
    public SaveController saveC;

    private void Awake()
    {
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

    public void GoToLevel(int level)
    {
        //SceneManager.LoadScene("Level"+level);
        SceneManager.LoadScene("Prototype");
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
}
