using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private Collider coll;
    InputController InputController;
    PerfectCatch perfectCatch;
    Rigidbody[] ball;
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
    public int numberOfBalls;
    // Start is called before the first frame update
    void Start()
    {
        coll = gameObject.GetComponent<Collider>();
        InputController = GetComponent<InputController>();
        isInCatchZone = true;
        perfectCatch = GetComponentInChildren<PerfectCatch>();
        scoreManager = FindObjectOfType<ScoreManager>();
        ball = new Rigidbody[4];
        numberOfBalls = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        numberOfBalls++;
        ball[numberOfBalls-1] =  other.gameObject.GetComponent<Rigidbody>();
        if (numberOfBalls >= 1)
        {
            isInCatchZone = true;
            indication.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        numberOfBalls--;
        ball[numberOfBalls] = null;
        if (numberOfBalls == 0)
        {
            isInCatchZone = false;
            indication.SetActive(false);
        }
    }
    public void Throw(string hand, string throwType)
    {
        if (isInCatchZone)
        {
            scoreManager.IncrementScore();
            var currentBall = ball[numberOfBalls-1];
            currentBall.isKinematic = true;
            if (perfectCatch.perfectCatch)
            {
                Debug.Log("Perfect Catch");
            }
            switch (throwType)
            {
                case "Up":
                    if (hand == "Left")
                    {
                        currentBall.isKinematic = false;
                        currentBall.transform.position = Vector3.Lerp(currentBall.transform.position, leftHandPosition.position, 0.5f);
                        currentBall.AddForce(throwUpLeftHand * throwUpForce);
                    }
                    else
                    {
                        currentBall.isKinematic = false;
                        currentBall.transform.position = Vector3.Lerp(currentBall.transform.position, rightHandPosition.position, 0.5f);
                        currentBall.AddForce(throwUpRightHand * throwUpForce);
                    }
                    break;
                case "Down":
                    if (hand == "Left")
                    {
                        currentBall.isKinematic = false;
                        currentBall.transform.position = Vector3.Lerp(currentBall.transform.position, leftHandPosition.position, 0.5f);
                        currentBall.AddForce(throwDownLeftHand * throwDownForce);
                    }
                    else
                    {
                        currentBall.isKinematic = false;
                        currentBall.transform.position = Vector3.Lerp(currentBall.transform.position, rightHandPosition.position, 0.5f);
                        currentBall.AddForce(throwDownRightHand * throwDownForce);
                    }
                    break;
                case "Left":
                    currentBall.isKinematic = false;
                    currentBall.transform.position = Vector3.Lerp(currentBall.transform.position, rightHandPosition.position, 0.5f);
                    currentBall.AddForce(throwLeft * throwSideForce);
                    break;
                case "Right":
                    currentBall.isKinematic = false;
                    currentBall.transform.position = Vector3.Lerp(currentBall.transform.position, leftHandPosition.position, 0.5f);
                    currentBall.AddForce(throwRight * throwSideForce);
                    break;
            }
        }
    }
}