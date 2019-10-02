using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class startMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if ((int.Parse(sceneName.Substring(sceneName.Length - 1, 1)))%2 == 0)
        AkSoundEngine.PostEvent("InGameMusic1_event", gameObject);
       else
        AkSoundEngine.PostEvent("InGameMusic2_event", gameObject);
    }
}



