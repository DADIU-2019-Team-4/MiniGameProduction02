using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    private Canvas pauseMenu;
    public bool isPaused=false;


    private void Awake()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("pauseMenu").GetComponent<Canvas>();
        Debug.Log(pauseMenu.gameObject.name);
    }

    void Start()
    {
        
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

    }
}
