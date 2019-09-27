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
        if (collision.gameObject.tag == "Ball")
            BallController.BallDropped();
    }
}
