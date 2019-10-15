using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            AkSoundEngine.PostEvent("BallBounce_event", gameObject);
        }
    }
}
