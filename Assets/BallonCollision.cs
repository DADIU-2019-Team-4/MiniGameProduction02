using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Balloon")
        {
            AkSoundEngine.PostEvent("BalloonFly_event", gameObject);
        }
    }
}
