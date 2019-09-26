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
        
        if (other.gameObject.tag == "Sabre")
        {
            Debug.Log("Stuck");
            ballController.StickToFloor(other.gameObject);
        }
    }
}
