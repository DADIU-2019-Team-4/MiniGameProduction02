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
        Left,
        Right
    }

    // Start is called before the first frame update
    void Awake()
    {
        BallController = FindObjectOfType<BallController>();
        ScoreController = FindObjectOfType<ScoreController>();
    }

    public void MidThrow(HandType hand)
    {
        AudioController.PlaySFX();

        if (hand == HandType.Left)
            leftHand.Throw("Left", "Right");
        else
            rightHand.Throw("Right", "Left");
    }

    public void HighThrow(HandType hand)
    {
        AudioController.PlaySFX();

        if (hand == HandType.Left)
            leftHand.Throw("Left", "Up");
        else
            rightHand.Throw("Right", "Up");
    }

    public void FloorBounce(HandType hand)
    {
        AudioController.PlaySFX();

        if (hand == HandType.Left)
            leftHand.Throw("Left", "Down");
        else
            rightHand.Throw("Right", "Down");
    }
}
