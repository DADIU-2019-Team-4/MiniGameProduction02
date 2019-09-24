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
    public Vector3 throwUpLeftHand;
    public Vector3 throwUpRightHand;
    public Vector3 throwDownLeftHand;
    public Vector3 throwDownRightHand;
    public Vector3 throwLeft;
    public Vector3 throwRight;
    public float throwUpForce;
    public float throwDownForce;
    public float throwSideForce;
    public Transform rightHandPosition;
    public Transform leftHandPosition;

    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        coll = gameObject.GetComponent<Collider>();
        InputController = GetComponent<InputController>();
        isInCatchZone = true;
        perfectCatch = GetComponentInChildren<PerfectCatch>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        ball =  other.gameObject.GetComponent<Rigidbody>();
        isInCatchZone = true;
        indication.SetActive(true);

    }

    private void OnTriggerExit(Collider other)
    {
        ball = null;
        isInCatchZone = false;
        indication.SetActive(false);
    }
    public void Throw(string hand, string throwType)
    {
        if (isInCatchZone)
        {
            scoreManager.IncrementScore();

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
                        ball.transform.position = Vector3.Lerp(ball.transform.position, leftHandPosition.position, 0.5f);
                        ball.AddForce(throwUpLeftHand * throwUpForce);
                    }
                    else
                    {
                        ball.isKinematic = false;
                        ball.transform.position = Vector3.Lerp(ball.transform.position, rightHandPosition.position, 0.5f);
                        ball.AddForce(throwUpRightHand * throwUpForce);
                    }
                    break;
                case "Down":
                    if (hand == "Left")
                    {
                        ball.isKinematic = false;
                        ball.transform.position = Vector3.Lerp(ball.transform.position, leftHandPosition.position, 0.5f);
                        ball.AddForce(throwDownLeftHand * throwDownForce);
                    }
                    else
                    {
                        ball.isKinematic = false;
                        ball.transform.position = Vector3.Lerp(ball.transform.position, rightHandPosition.position, 0.5f);
                        ball.AddForce(throwDownRightHand * throwDownForce);
                    }
                    break;
                case "Left":
                    ball.isKinematic = false;
                    ball.transform.position = Vector3.Lerp(ball.transform.position, rightHandPosition.position, 0.5f);
                    ball.AddForce(throwLeft * throwSideForce);
                    break;
                case "Right":
                    ball.isKinematic = false;
                    ball.transform.position = Vector3.Lerp(ball.transform.position, leftHandPosition.position, 0.5f);
                    ball.AddForce(throwRight * throwSideForce);
                    break;
            }
        }
    }
}