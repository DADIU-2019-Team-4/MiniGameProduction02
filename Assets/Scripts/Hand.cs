using UnityEngine;

public class Hand : MonoBehaviour
{
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
    public float throwForce;

    private ScoreController scoreController;
    private ProgressionController progressionController;

    private void Awake()
    {
        perfectCatch = GetComponentInChildren<PerfectCatch>();
        scoreController = FindObjectOfType<ScoreController>();
        progressionController = FindObjectOfType<ProgressionController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ball = other.gameObject.GetComponent<Rigidbody>();
            isInCatchZone = true;
            indication.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ball = null;
            isInCatchZone = false;
            indication.SetActive(false);
        }
    }

    public void Throw(string hand, string throwType)
    {
        if (isInCatchZone)
        {
            scoreController.IncrementScore(ScoreController.CatchType.normalCatch);
            progressionController.UpdateProgression(ProgressionController.CatchType.normalCatch);

            ball.isKinematic = true;
            if (perfectCatch.perfectCatch)
            {
                Debug.Log("Perfect Catch");
                scoreController.IncrementScore(ScoreController.CatchType.perfectCatch);
                progressionController.UpdateProgression(ProgressionController.CatchType.perfectCatch);
            }
            switch (throwType)
            {
                case "Up":
                    if (hand == "Left")
                    {
                        ball.isKinematic = false;
                        ball.AddForce(throwUpLeftHand * throwForce);
                    }
                    else
                    {
                        ball.isKinematic = false;
                        ball.AddForce(throwUpRightHand * throwForce);
                    }
                    break;
                case "Down":
                    if (hand == "Left")
                    {
                        ball.isKinematic = false;
                        ball.AddForce(throwDownLeftHand * throwForce);
                    }
                    else
                    {
                        ball.isKinematic = false;
                        ball.AddForce(throwDownRightHand * throwForce);
                    }
                    break;
                case "Left":
                    ball.isKinematic = false;
                    ball.AddForce(throwLeft * throwForce);
                    break;
                case "Right":
                    ball.isKinematic = false;
                    ball.AddForce(throwRight * throwForce);
                    break;
            }
        }
    }
}