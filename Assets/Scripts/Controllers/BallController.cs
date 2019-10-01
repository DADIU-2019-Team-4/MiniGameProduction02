using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private ScoreController ScoreController;
    private SceneController SceneController;
    private TutorialManager TutorialManager;
    private CollectionItemSpawner CollectionItemSpawner;
    private LifeManager LifeManager;


    public GameObject[] BallPrefab;
    private int numberOfBalls;
    public float distanceBetweenSpawnedBalls;

    private readonly List<GameObject> Balls = new List<GameObject>();
    private readonly List<GameObject> ballsInCatchZone = new List<GameObject>();

    private int throwCount;
    public Vector3 lerpRightHandTarget;
    public Vector3 lerpLeftHandTarget;



    // TODO: Make these private and programmatically retrieve these.
    // These should NOT available in the editor; This is bloat for Level Designers.
    public Transform leftHand;
    public Transform rightHand;
    public GameObject leftPerfectCatch;
    public GameObject rightPerfectCatch;

    public Transform HighThrowForce;
    public Transform MidThrowForce;
    public Transform FloorBounceForce;
    public float throwForceMultiplier;
    public float gravityYaxis;

    public float TimeScale;
    public float BalloonFloatStrength = 0.5f;
    public Vector3 balloonThrowDown;
    public Vector3 ballonThrowMid;
    public int ballSelectorInt = 0;
    public float delayTime;
    [Header("Falling Ball Settings")]
    public float slowDownTime;
    public bool spawnInRandomHand;
    public float respawnYAxis;
    private bool _tutorialLevel;
    public float spawnAllIntervals;

    public bool IsAlwaysPerfectCatch { get; set; }

    void Awake()
    {
        Time.timeScale = TimeScale;
        ScoreController = FindObjectOfType<ScoreController>();
        SceneController = FindObjectOfType<SceneController>();
        LifeManager = FindObjectOfType<LifeManager>();
        Physics.gravity = new Vector3(0, gravityYaxis, 0);
        if (FindObjectOfType<TutorialManager>() != null)
        {
            TutorialManager = FindObjectOfType<TutorialManager>();
            CollectionItemSpawner = FindObjectOfType<CollectionItemSpawner>();
            _tutorialLevel = true;
        }
        else
            _tutorialLevel = false;
    }


    void Update()
    {
        //foreach (GameObject item in Balls)
        //if (item.tag == "Balloon")
        // item.GetComponent<Rigidbody>().AddForce(new Vector3(0, BalloonFloatStrength, 0));
    }

    private void Start()
    {
        numberOfBalls = BallPrefab.Length;
        SpawnBalls(numberOfBalls);
        AkSoundEngine.PostEvent("ColliderLeft_event", gameObject);
        AkSoundEngine.SetRTPCValue("leftCollider", 0.0f);
        AkSoundEngine.PostEvent("ColliderRight_event", gameObject);
        AkSoundEngine.SetRTPCValue("rightCollider", 0.0f);
    }

    #region Ball Creation/Deletion

    private void SpawnBalls(int number)
    {
        for (int i = 0; i < number; i++)
            StartCoroutine(SpawnRoutine(i));
    }

    IEnumerator SpawnRoutine(int i)
    {
        yield return new WaitForSeconds((1 + i) * spawnAllIntervals);
        AddBall(new Vector3(rightHand.transform.position.x, respawnYAxis, 0), i, true);
    }

    private void AddBall(Vector3 where, int prefabInt, bool isDropped)
    {
        if (SceneController.GameEnded)
            return;

        GameObject ball = Instantiate(BallPrefab[prefabInt], where, rightHand.transform.rotation);
        if (isDropped)
        {
            ball.GetComponent<Rigidbody>().isKinematic = false;
            ball.GetComponent<Rigidbody>().drag = slowDownTime;
        }
        Balls.Add(ball);
        ballSelectorInt++;
    }

    private void RemoveBall(GameObject ball)
    {
        Balls.Remove(ball);
        Destroy(ball);
    }

    public void Restart()
    {
        ballsInCatchZone.Clear();
        while (Balls.Count != 0)
            RemoveBall(Balls[0]);
        SpawnBalls(numberOfBalls);
    }


    #endregion

    #region Ball Collision Detection

    public void BallEntersHand(Collider collider)
    {
        var ball = collider.gameObject;
        if (!ballsInCatchZone.Contains(ball))
            ballsInCatchZone.Add(ball);
        PlayDistanceSound(ball);

        Debug.Log("Ball has entered the hand. I repeat Ball has entered the hand");
        
        if (_tutorialLevel)
        {
            if (TutorialManager._previousTutorialStage < 3)
                TutorialManager.EnableTutorialUI();
            if (TutorialManager._previousTutorialStage == 3)
            {
                CollectionItemSpawner.SpawnTutorialObject();
                TutorialManager.EnableTutorialUI();
            }
        }
    }

    public void BallLeavesHand(Collider collider)
    {
        var ball = collider.gameObject;
        if (ballsInCatchZone.Contains(ball))
            ballsInCatchZone.Remove(ball);

        CheckIfEndOfAnimation(ball);
        Debug.Log("Ball is out of hand, carefull");
    }

    public void BallDropped(GameObject obj)
    {
        RemoveBall(obj);
        if (!_tutorialLevel)
        {
            LifeManager.CurrentLives--;
            LifeManager.UpdateLives();
        }
        throwCount = 0;
        //ballsInCatchZone.Clear();
        StartCoroutine(Delay(delayTime));
        if (spawnInRandomHand)
        {
            if (Mathf.CeilToInt(Random.Range(0f, 1f)) == 0)
                AddBall(new Vector3(rightHand.transform.position.x, respawnYAxis, 0), 0, true);
            else
                AddBall(new Vector3(leftHand.transform.position.x, respawnYAxis, 0), 0, true);
        }
        else
        {
            if (obj.transform.position.x > 0)
                AddBall(new Vector3(leftHand.transform.position.x, respawnYAxis, 0), 0, true);
            else
                AddBall(new Vector3(rightHand.transform.position.x, respawnYAxis, 0), 0, true);
        }

    }

    #endregion


    #region Ball Throwing
    // These functions rely on that the two hands are at positive/negative X positions!

    public void Throw(ViolaController.ThrowType throwType, ViolaController.HandType hand)
    {

        var ball = GetBallToThrow(hand);
        if (ball == null) return;

        Rigidbody ballRigidBody = ball.GetComponent<Rigidbody>();
        

        AkSoundEngine.SetRTPCValue("rightCollider", 0.0f);

        IEnumerator coroutine = LerpToHandAndPlayAnim(throwType, hand, ball);

        StartCoroutine(coroutine);

        if (hand == ViolaController.HandType.Left)
        {
            AkSoundEngine.PostEvent("ColliderLeft_event", gameObject);

        }
        else if (hand == ViolaController.HandType.Right)
        {
            AkSoundEngine.PostEvent("ColliderRight_event", gameObject);

        }



        ballRigidBody.isKinematic = true;

        ball.GetComponent<Ball>().wasPerfectlyThrown = GotPerfectCatch(ball);

        Vector3 throwVector = GetThrowForce(throwType, ball);
        SetThrowDirection(hand, ref throwVector, ballRigidBody);
        ballRigidBody.isKinematic = false;

        if (ballRigidBody.drag > 0)
            ballRigidBody.drag = 0;

        ballRigidBody.AddForce(throwVector);
        BallLeavesHand(ball.GetComponent<Collider>());
        throwCount++;

        if (throwCount >= numberOfBalls)
            SceneController.IsPlaying = true;
    }

    private bool GotPerfectCatch(GameObject ball)
    {
        if (IsAlwaysPerfectCatch)
            return true;

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
            currentBall.transform.position = Vector3.MoveTowards(currentBall.transform.position, leftHand.position, 0.7f);
        }
        else
            currentBall.transform.position = Vector3.MoveTowards(currentBall.transform.position, rightHand.position, 0.7f);

    }

    private Vector3 GetThrowForce(ViolaController.ThrowType throwType, GameObject juggledItem)
    {
        switch (throwType)
        {
            case ViolaController.ThrowType.HighThrow:

                return HighThrowForce.localPosition * throwForceMultiplier;

            case ViolaController.ThrowType.FloorBounce:
                if (juggledItem.tag == "Balloon")
                    return new Vector3(0, balloonThrowDown.y * throwForceMultiplier, 0) * BalloonFloatStrength; // No X-value
                else
                    return FloorBounceForce.localPosition * throwForceMultiplier;

            case ViolaController.ThrowType.MidThrow:
                if (juggledItem.tag == "Balloon")
                    return ballonThrowMid * throwForceMultiplier * BalloonFloatStrength;
                else
                    return MidThrowForce.localPosition * throwForceMultiplier;

            case ViolaController.ThrowType.None:
            default:
                return Vector3.zero;
        }
    }

    public void StickToFloor(GameObject ball)
    {
        ball.GetComponent<Rigidbody>().isKinematic = true;
    }

    #endregion

    #region Distance between perfect catch and a ball
    public void PlayDistanceSound(GameObject obj)
    {
        Rigidbody rigid = obj.GetComponent<Rigidbody>();
        var velocity = obj.transform.InverseTransformDirection(rigid.velocity);
        float yAxis = velocity.y;

        if (yAxis < 0 && obj.transform.position.y > rightHand.position.y)
        {
            if (obj.transform.position.x < 0)
            {
                float distance = Vector3.Distance(rightHand.position, obj.transform.position);

                AkSoundEngine.PostEvent("ColliderRight_event", gameObject);
                AkSoundEngine.SetRTPCValue("rightCollider", 1 - distance);
            }
            else
            {
                float distance = Vector3.Distance(leftHand.position, obj.transform.position);
                AkSoundEngine.PostEvent("ColliderLeft_event", gameObject);
                AkSoundEngine.SetRTPCValue("leftCollider", 1 - distance);
            }
        }
    }
    #endregion

    IEnumerator Delay(float seconds)
    {
        Debug.Log("Goes to delay");
        yield return new WaitForSeconds(seconds);
    }

    /*
    public void PlayCorrectAnimation(ViolaController.ThrowType throwType, ViolaController.HandType hand, GameObject ball)
    {
        Animator ballAnimator = ball.GetComponent<Animator>();

        if (hand == ViolaController.HandType.Left)
        {

            if(throwType == ViolaController.ThrowType.HighThrow)
            {
                ballAnimator.Play("RightToLeftUP", 0, 0f);
            }
            else if (throwType == ViolaController.ThrowType.MidThrow)
            {

            }
            else if (throwType == ViolaController.ThrowType.FloorBounce)
            {

            }

            
            Debug.Log("gottem");

        }
        else if (hand == ViolaController.HandType.Right)
        {

            if (throwType == ViolaController.ThrowType.HighThrow)
            {
                ballAnimator.Play("LeftToRightUP", 0, 0f);
            }
            else if (throwType == ViolaController.ThrowType.MidThrow)
            {
                ballAnimator.Play("LeftToRightMiddle", 0, 0f);
                Debug.Log("gottem for real");
            }
            else if (throwType == ViolaController.ThrowType.FloorBounce)
            {
                ballAnimator.Play("LeftToRightDown", 0, 0f);

            }
            Debug.Log("gottem for real");

        }

    }
    */

    public bool CheckIfEndOfAnimation(GameObject ball)
    {
        if (ball.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
        {
            EnablePhysicsAndApplyForce(ball, new Vector3(1,0,0), 50f);
            return true;
        }
        else
        {
            return false;
        }

        
    }
    public void EnablePhysics(GameObject ball)
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        Animator anim = ball.GetComponent<Animator>();

        anim.enabled = false;
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    public void EnablePhysicsAndApplyForce(GameObject ball, Vector3 dir, float power)
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        Animator anim = ball.GetComponent<Animator>();

        anim.enabled = false;
        rb.isKinematic = false;
        rb.useGravity = true;

        Vector3 force = dir * power;

        rb.AddForce(force);
    }


    IEnumerator LerpToHandAndPlayAnim(ViolaController.ThrowType throwType, ViolaController.HandType hand, GameObject ball)
    {

        Animator ballAnimator = ball.GetComponent<Animator>();
        Debug.Log("Lerping");
        float duration = 0.02f;


        if (hand == ViolaController.HandType.Left)
        {
            float journey = 0f;
            Vector3 currentpos = ball.transform.position;

            ballAnimator.enabled = false;
            while (journey <= duration)
            {
                journey = journey + Time.deltaTime;
                float percent = Mathf.Clamp01(journey / duration);
                ball.transform.position = Vector3.Lerp(currentpos, lerpLeftHandTarget, percent);

                yield return null;
            }
            
            
            yield return new WaitForSeconds(duration);
            ballAnimator.enabled = true;

            if (throwType == ViolaController.ThrowType.HighThrow)
            {
                ballAnimator.Play("RightToLeftUP", 0, 0f);
            }
            else if (throwType == ViolaController.ThrowType.MidThrow)
            {
                ballAnimator.Play("RightToLeftMiddle", 0, 0f);
            }
            else if (throwType == ViolaController.ThrowType.FloorBounce)
            {
                ballAnimator.Play("RightToLeftDown", 0, 0f);
            }


            Debug.Log("gottem");

        }
        else if (hand == ViolaController.HandType.Right)
        {
            
            float journey = 0f;
            Vector3 currentpos = ball.transform.position;

            ballAnimator.enabled = false;
            while (journey <= duration)
            {
                journey = journey + Time.deltaTime;
                float percent = Mathf.Clamp01(journey / duration);
                ball.transform.position = Vector3.Lerp(currentpos, lerpRightHandTarget, percent);

                yield return null;
            }


            yield return new WaitForSeconds(duration);
            ballAnimator.enabled = true;

            if (throwType == ViolaController.ThrowType.HighThrow)
            {
                ballAnimator.Play("LeftToRightUP", 0, 0f);
            }
            else if (throwType == ViolaController.ThrowType.MidThrow)
            {
                ballAnimator.Play("LeftToRightMiddle", 0, 0f);
                Debug.Log("gottem for real");
            }
            else if (throwType == ViolaController.ThrowType.FloorBounce)
            {
                ballAnimator.Play("LeftToRightDown", 0, 0f);

            }
            Debug.Log("gottem for real");

        }

        yield return null;
    }
}
