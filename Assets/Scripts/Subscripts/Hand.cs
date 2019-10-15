using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private BallController BallController;

    private void Awake()
    {
        BallController = FindObjectOfType<BallController>();
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ball")
            BallController.BallEntersHand(other);
    }

    private void OnTriggerExit(Collider other)
    {
        BallController.BallLeavesHand(other);
    }

}
