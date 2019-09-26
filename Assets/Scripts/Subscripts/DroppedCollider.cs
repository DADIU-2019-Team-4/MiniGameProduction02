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
        if (collision.gameObject.tag == "Ball" || collision.gameObject.tag == "Balloon" || collision.gameObject.tag == "Sabre")
            BallController.BallDropped();
    }
}
