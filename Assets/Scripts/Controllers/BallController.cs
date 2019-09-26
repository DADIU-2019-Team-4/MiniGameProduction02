using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private ScoreController ScoreController;
    private ProgressionController ProgressionController;
    private SceneController SceneController;


    public GameObject[] BallPrefab;
    public int numberOfBalls;
    public float distanceBetweenSpawnedBalls;

    private readonly List<GameObject> Balls = new List<GameObject>();
    private readonly List<GameObject> ballsInCatchZone = new List<GameObject>();

    private int throwCount;


    // TODO: Make these private and programmatically retrieve these.
    // These should NOT available in the editor; This is bloat for Level Designers.
    public Transform leftHand;
    public Transform rightHand;
    public GameObject leftPerfectCatch;
    public GameObject rightPerfectCatch;

    public Vector3 throwUpRightHand;
    public Vector3 throwDownRightHand;
    public Vector3 throwLeft;
    public float throwUpForce;
    public float throwDownForce;
    public float throwSideForce;
    public float gravityYaxis;

    public float TimeScale;

    void Awake()
    {
        Time.timeScale = TimeScale;
        ScoreController = FindObjectOfType<ScoreController>();
        ProgressionController = FindObjectOfType<ProgressionController>();
        SceneController = FindObjectOfType<SceneController>();
        Physics.gravity = new Vector3(0, gravityYaxis, 0);
    }

    private void Start()
    {
        SpawnBalls(numberOfBalls);
    }

    #region Ball Creation/Deletion

    private void SpawnBalls(int number)
    {
        Vector3 spawnPosition;

        for (int i = 0; i < number; i++)
        {
            if (i % 2 == 0)
                spawnPosition = new Vector3(rightHand.transform.position.x + distanceBetweenSpawnedBalls * (Mathf.Round(i / 2) - 1), rightHand.transform.position.y, rightHand.transform.position.z);
            else
                spawnPosition = new Vector3(leftHand.transform.position.x - distanceBetweenSpawnedBalls * (Mathf.Round(i / 2) - 1), leftHand.transform.position.y, leftHand.transform.position.z);
            AddBall(spawnPosition);
        }
    }

    private void AddBall(Vector3 where)
    {
        GameObject ball = Instantiate(BallPrefab[Random.Range(0,BallPrefab.Length-1)], where, rightHand.transform.rotation);
        Balls.Add(ball);
    }

    private void RemoveBall(GameObject ball)
    {
        Balls.Remove(ball);
        Destroy(ball);
    }

    #endregion

    #region Ball Collision Detection

    public void BallEntersHand(Collider collider)
    {
        var ball = collider.gameObject;
        if (!ballsInCatchZone.Contains(ball))
            ballsInCatchZone.Add(ball);
    }

    public void BallLeavesHand(Collider collider)
    {
        var ball = collider.gameObject;
        if (ballsInCatchZone.Contains(ball))
            ballsInCatchZone.Remove(ball);

    }

    public void BallDropped()
    {
        ScoreController.DroppedBall();
        throwCount = 0;
        ballsInCatchZone.Clear();

        while (Balls.Count != 0)
            RemoveBall(Balls[0]);

        StartCoroutine(Delay(0.5f));

        SpawnBalls(numberOfBalls);
    }

    #endregion


    #region Ball Throwing
    // These functions rely on that the two hands are at positive/negative X positions!

    public void Throw(ViolaController.ThrowType throwType, ViolaController.HandType hand)
    {
        var ball = GetBallToThrow(hand);
        if (ball == null) return;

        Rigidbody ballRigidBody = ball.GetComponent<Rigidbody>();
        ballRigidBody.isKinematic = true;

        var catchType = GotPerfectCatch(ball) ? ScoreController.CatchType.Perfect : ScoreController.CatchType.Normal;
        ScoreController.IncrementScore(catchType);
        ProgressionController.UpdateProgression(catchType);

        Vector3 throwVector = GetThrowForce(throwType);
        SetThrowDirection(hand, ref throwVector, ballRigidBody);
        ballRigidBody.isKinematic = false;
        ballRigidBody.AddForce(throwVector);
        BallLeavesHand(ball.GetComponent<Collider>());
        throwCount++;

        if (throwCount >= 3)
            SceneController.IsPlaying = true;
    }

    private bool GotPerfectCatch(GameObject ball)
    {
        if (ball.transform.position.x < 0 &&
            rightPerfectCatch.GetComponent<PerfectCatch>().In.Contains(ball))
            return true;
        if (ball.transform.position.x > 0 &&
            leftPerfectCatch.GetComponent<PerfectCatch>().In.Contains(ball))
            return true;

        return false;
    }

    private GameObject GetBallToThrow(ViolaController.HandType hand)
    {
        foreach (GameObject ball in ballsInCatchZone)
            if ((hand == ViolaController.HandType.Right && ball.transform.position.x < 0)
                || (hand == ViolaController.HandType.Left && ball.transform.position.x > 0))
                return ball;
        return null;
    }

    private void SetThrowDirection(ViolaController.HandType handType, ref Vector3 throwAngle, Rigidbody currentBall)
    {
        if (handType == ViolaController.HandType.Left)
        {
            throwAngle.x *= -1;
            currentBall.transform.position = Vector3.MoveTowards(currentBall.transform.position, leftHand.position, 0.5f);
        }
        else
            currentBall.transform.position = Vector3.MoveTowards(currentBall.transform.position, rightHand.position, 0.5f);
    }

    private Vector3 GetThrowForce(ViolaController.ThrowType throwType)
    {
        switch (throwType)
        {
            case ViolaController.ThrowType.HighThrow:

                return throwUpRightHand * throwUpForce;

            case ViolaController.ThrowType.FloorBounce:
                return throwDownRightHand * throwDownForce;

            case ViolaController.ThrowType.MidThrow:
                return throwLeft * throwSideForce;

            case ViolaController.ThrowType.None:
            default:
                return Vector3.zero;
        }
    }

    #endregion

    IEnumerator Delay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
