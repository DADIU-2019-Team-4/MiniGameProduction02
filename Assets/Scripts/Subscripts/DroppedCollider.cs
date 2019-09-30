using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedCollider : MonoBehaviour
{
    BallController BallController;

    // Start is called before the first frame update
    void Awake()
    {
        BallController = FindObjectOfType<BallController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Enter The collision");
        if (collision.gameObject.tag == "Ball" || collision.gameObject.tag == "Balloon" || collision.gameObject.tag == "Sabre")
            if (collision.gameObject.tag == "Balloon")
                AkSoundEngine.PostEvent("BalloonPop_event", gameObject);
        BallController.BallDropped(collision.gameObject);
    }

}
