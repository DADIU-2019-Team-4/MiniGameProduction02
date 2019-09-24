using UnityEngine;

public class ViolaController : MonoBehaviour
{
    // Keeps track of all functionality in regards to Viola.

    private BallController BallController;
    private ScoreController ScoreController;

    public Hand leftHand;
    public Hand rightHand;

    public enum HandType
    {
        None,
        Left,
        Right
    }

    public enum ThrowType
    {
        None,
        HighThrow,
        MidThrow,
        FloorBounce,
    }

    // Start is called before the first frame update
    void Awake()
    {
        BallController = FindObjectOfType<BallController>();
        ScoreController = FindObjectOfType<ScoreController>();
    }

    void Start()
    {
        leftHand.SetHandType(HandType.Left);
        rightHand.SetHandType(HandType.Right);
    }

    public void Throw(ThrowType throwType, HandType handType)
    {
        Hand hand = null;

        if (handType == HandType.Left)
            hand = leftHand;
        else if (handType == HandType.Right)
            hand = rightHand;

        hand.Throw(throwType);
    }


}
