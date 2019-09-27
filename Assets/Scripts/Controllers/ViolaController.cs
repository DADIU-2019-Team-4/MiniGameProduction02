using System;
using UnityEngine;

public class ViolaController : MonoBehaviour
{
    // Keeps track of all functionality in regards to Viola.

    private BallController BallController;
    //private ScoreController ScoreController;
    private Animator ViolaAnimator;

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
        ViolaAnimator = gameObject.GetComponent<Animator>();
    }

    public void Throw(ThrowType throwType, HandType handType)
    {
        BallController.Throw(throwType, handType);

        // Compose Trigger's name (direction + hand)
        String TriggerName = "throw";
        TriggerName +=
            (throwType == ThrowType.HighThrow) ? "Up" :
            (throwType == ThrowType.MidThrow) ? "Side" :
            (throwType == ThrowType.FloorBounce) ? "Down" :
            "INVALID";

        // Note that Triggers L&R follow user's perspective, while Hands Viola's
        TriggerName +=
            (handType == HandType.Left) ? "R" :
            (handType == HandType.Right) ? "L" :
            "INVALID";

        // Trigger throwing Animation
        ViolaAnimator.SetTrigger(TriggerName);

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
