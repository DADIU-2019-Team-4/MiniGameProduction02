using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      if(Random.Range(0f,1f)> 0.5f)
        AkSoundEngine.PostEvent("InGameMusic1_event", gameObject);
       else
        AkSoundEngine.PostEvent("InGameMusic2_event", gameObject);
    }
}



