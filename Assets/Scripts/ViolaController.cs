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

    public void Throw(ThrowType throwType, HandType hand)
    {
        switch (throwType)
        {
            case ThrowType.HighThrow:
                HighThrow(hand);
                break;

            case ThrowType.MidThrow:
                MidThrow(hand);
                break;

            case ThrowType.FloorBounce:
                FloorBounce(hand);
                break;
        }
    }

    private void MidThrow(HandType hand)
    {
        AudioController.PlaySFX();

        if (hand == HandType.Left)
            leftHand.Throw("Left", "Right");
        else
            rightHand.Throw("Right", "Left");
    }

    private void HighThrow(HandType hand)
    {
        AudioController.PlaySFX();

        if (hand == HandType.Left)
            leftHand.Throw("Left", "Up");
        else
            rightHand.Throw("Right", "Up");
    }

    private void FloorBounce(HandType hand)
    {
        AudioController.PlaySFX();

        if (hand == HandType.Left)
            leftHand.Throw("Left", "Down");
        else
            rightHand.Throw("Right", "Down");
    }
}
