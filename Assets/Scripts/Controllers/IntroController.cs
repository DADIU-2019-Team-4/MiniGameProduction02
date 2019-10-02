using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    private DirectorController DirectorController;

    // Start is called before the first frame update
    void Start()
    {
        DirectorController = FindObjectOfType<DirectorController>();

        // Initiate Manager's intro Animation
        AkSoundEngine.PostEvent("VoiceOverWelcome_event", gameObject);
        DirectorController.PlayIntroAnimation();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Finish()
    {
        // After the Intro is complete, start the next Scene
        SceneManager.LoadScene("Level1");
    }
}
