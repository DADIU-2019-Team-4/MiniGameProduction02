using UnityEngine;

public class Hand : MonoBehaviour
{
    private PerfectCatch perfectCatch;
    private Rigidbody ball;
    private bool isInCatchZone;

    public GameObject indication;
    public Vector3 throwUpRightHand;
    public Vector3 throwDownRightHand;
    public Vector3 throwLeft;
    public float throwForce;

    private ViolaController.HandType HandType;

    private ScoreController scoreController;

    private void Awake()
    {
        perfectCatch = GetComponentInChildren<PerfectCatch>();
        scoreController = FindObjectOfType<ScoreController>();
    }

    private void Start()
    {
        isInCatchZone = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        ball = other.gameObject.GetComponent<Rigidbody>();
        isInCatchZone = true;
        indication.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        ball = null;
        isInCatchZone = false;
        indication.SetActive(false);
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

        scoreController.IncrementScore();

        ball.isKinematic = true;

        if (perfectCatch.perfectCatch)
            Debug.Log("Perfect Catch");

        Vector3 throwAngle = GetForceAngle(throwType);

        SetThrowDirection(ref throwAngle);

        ball.isKinematic = false;

        ball.AddForce(throwAngle * throwForce);
        return;
    }


    private void SetThrowDirection(ref Vector3 throwAngle)
    {
        if (HandType == ViolaController.HandType.Left)
            throwAngle.x *= -1;
    }

    private Vector3 GetForceAngle(ViolaController.ThrowType throwType)
    {
        switch (throwType)
        {
            case ViolaController.ThrowType.HighThrow:
                return throwUpRightHand;

            case ViolaController.ThrowType.FloorBounce:
                return throwDownRightHand;

            case ViolaController.ThrowType.MidThrow:
                return throwLeft;

            default:
                return Vector3.zero;
        }
    }
}