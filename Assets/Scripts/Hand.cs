using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Hand : MonoBehaviour
{
    private PerfectCatch perfectCatch;
    //Rigidbody[] ball;
    public List <Rigidbody> ball;
    private bool isInCatchZone;

    public Vector3 throwUpRightHand;
    public Vector3 throwDownRightHand;
    public Vector3 throwLeft;
    public float throwUpForce;
    public float throwDownForce;
    public float throwSideForce;
    public Transform rightHandPosition;
    public Transform leftHandPosition;

    private ViolaController.HandType HandType;

    private ScoreController scoreController;
    private SceneController sceneController;
    private ProgressionController progressionController;
    private Rigidbody currentBall;

    public int numberOfBalls;
    private void Awake()
    {
        perfectCatch = GetComponentInChildren<PerfectCatch>();
        scoreController = FindObjectOfType<ScoreController>();
        sceneController = FindObjectOfType<SceneController>();
        progressionController = FindObjectOfType<ProgressionController>();
    }

    private void Start()
    {
        isInCatchZone = true;
        //ball = new Rigidbody[4];
        ball = new List<Rigidbody>();
        numberOfBalls = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hand" + numberOfBalls);
        numberOfBalls++;
        //ball[numberOfBalls - 1] = other.gameObject.GetComponent<Rigidbody>();
        ball.Add(other.gameObject.GetComponent<Rigidbody>());
        if (numberOfBalls >= 1)
        {
            isInCatchZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (numberOfBalls == 0)
        {
            isInCatchZone = false;
        }
    }

    public void SetHandType(ViolaController.HandType type)
    {
        if (HandType == ViolaController.HandType.None)
            HandType = type;
        else
            throw new System.Exception("Hand type is already set once.");
    }

    public void Throw(ViolaController.ThrowType throwType)
    {
        if (!isInCatchZone || throwType == ViolaController.ThrowType.None)
            return;

        sceneController.IsPlaying = true;
        currentBall = ball[0];
        currentBall.isKinematic = true;

        if (perfectCatch.perfectCatch)
        {
            Debug.Log("Perfect Catch");
            scoreController.IncrementScore(ScoreController.CatchType.perfectCatch);
            progressionController.UpdateProgression(ProgressionController.CatchType.perfectCatch);
        }
        else
        {
            scoreController.IncrementScore(ScoreController.CatchType.normalCatch);
            progressionController.UpdateProgression(ProgressionController.CatchType.normalCatch);
        }

        Vector3 throwAngle = GetForceAngle(throwType);

        SetThrowDirection(ref throwAngle, currentBall);
        currentBall.isKinematic = false;
        currentBall.AddForce(throwAngle);
        numberOfBalls--;
        ball.Remove(currentBall);
        return;
    }


    private void SetThrowDirection(ref Vector3 throwAngle, Rigidbody currentBall)
    {
        if (HandType == ViolaController.HandType.Left)
        {
            throwAngle.x *= -1;
            currentBall.transform.position = Vector3.Lerp(currentBall.transform.position, leftHandPosition.position, Time.fixedDeltaTime);
        }
        else
        {
            currentBall.transform.position = Vector3.Lerp(currentBall.transform.position, rightHandPosition.position, Time.fixedDeltaTime);
        }

    }

    private Vector3 GetForceAngle(ViolaController.ThrowType throwType)
    {
        switch (throwType)
        {
            case ViolaController.ThrowType.HighThrow:

                return throwUpRightHand * throwUpForce;

            case ViolaController.ThrowType.FloorBounce:
                return throwDownRightHand * throwDownForce;

            case ViolaController.ThrowType.MidThrow:
                return throwLeft * throwSideForce;

            default:
                return Vector3.zero;
        }
    }
}