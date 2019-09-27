using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private BallController ballController;

    public void Awake()
    {
        ballController = FindObjectOfType<BallController>();
    }

    public void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
        {
            case "Sabre":
                Debug.Log("Stuck");
                ballController.StickToFloor(other.gameObject);
                AkSoundEngine.PostEvent("SabreStuck_event", gameObject);
                break;
            case "Balloon":
                AkSoundEngine.PostEvent("BalloonBounce_event", gameObject);
                break;
            case "Ball":
                AkSoundEngine.PostEvent("BallBounce_event", gameObject);
                break;
            default:
                break;
        }
    }
}
