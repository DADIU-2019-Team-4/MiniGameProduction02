using UnityEngine;

public class ViolaController : MonoBehaviour
{
    // Keeps track of all functionality in regards to Viola.

    private BallController BallController;
    //private ScoreController ScoreController;

    //private Hand leftHand;
    //private Hand rightHand;

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
        //ScoreController = FindObjectOfType<ScoreController>();
    }

    void Start()
    {
        //AssignHands("LeftHandCollider");
    }

    public void Throw(ThrowType throwType, HandType handType)
    {
        BallController.Throw(throwType, handType);

        //Debug.Log(throwType.ToString() + " | " + handType.ToString());
        //return;

        //Hand hand = null;

        //if (handType == HandType.Left)
        //    hand = leftHand;
        //else if (handType == HandType.Right)
        //    hand = rightHand;

        //hand.Throw(throwType);
    }

    //private void AssignHands(string leftHandName)
    //{
    //    //var hands = GetComponentsInChildren<Hand>();

    //    //if (hands[0].name != leftHandName)
    //    //{
    //    //    rightHand = hands[0];
    //    //    leftHand = hands[1];
    //    //}
    //    //else
    //    //{
    //    //    rightHand = hands[1];
    //    //    leftHand = hands[0];
    //    //}

    //    //leftHand.SetHandType(HandType.Left);
    //    //rightHand.SetHandType(HandType.Right);
    //}


}
