using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private Collider coll;
    InputController InputController;
    PerfectCatch perfectCatch;
    Rigidbody ball;
    bool isInCatchZone;
    public GameObject indication;
    // Start is called before the first frame update
    void Start()
    {
        coll = gameObject.GetComponent<Collider>();
        InputController = GetComponent<InputController>();
        isInCatchZone = true;
        perfectCatch = GetComponentInChildren<PerfectCatch>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
        ball =  other.gameObject.GetComponent<Rigidbody>();
        isInCatchZone = true;
        indication.SetActive(true);

    }

    private void OnTriggerExit(Collider other)
    {
        ball = null;
        isInCatchZone = true;
        indication.SetActive(false);
    }
    public void Throw(string hand, string throwType)
    {
        if (isInCatchZone)
        {
            ball.isKinematic = true;
            if (perfectCatch.perfectCatch)
            {
                Debug.Log("Perfect Catch");
            }
            switch (throwType)
            {
                case "Up":
                    if (hand == "Left")
                    {
                        ball.isKinematic = false;
                        ball.AddForce(new Vector3(-1, 7, 0) * 72f);
                    }
                    else
                    {
                        ball.isKinematic = false;
                        ball.AddForce(new Vector3(1, 7, 0) * 72f);
                    }
                    break;
                case "Down":
                    if (hand == "Left")
                    {
                        ball.isKinematic = false;
                        ball.AddForce(new Vector3(-4f, -1, 0) * 72f);
                    }
                    else
                    {
                        ball.isKinematic = false;
                        ball.AddForce(new Vector3(4f, -1, 0) * 72f);
                    }
                    break;
                case "Left":
                    ball.isKinematic = false;
                    ball.AddForce(new Vector3(4, 1.7f, 0) * 72f);
                    break;
                case "Right":
                    ball.isKinematic = false;
                    ball.AddForce(new Vector3(-4, 1.7f, 0) * 72f);
                    break;
            }
        }
    }
}