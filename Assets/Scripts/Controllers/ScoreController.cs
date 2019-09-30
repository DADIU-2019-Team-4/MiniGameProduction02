using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private SceneController SceneController;

    public Text scoreText;
    [SerializeField]
    private int score;
    private int receivedPoints;
    [SerializeField]
    private int itemHitCombo = 0;

    //[SerializeField]
    //private int normalCatchPoints = 1;
    //[SerializeField]
    //private int perfectCatchPoints = 3;

    public enum CatchType { Normal, Perfect, Failed }

    private void Awake()
    {
        SceneController = FindObjectOfType<SceneController>();
    }

    public void IncrementScore(bool wasPerfectlyThrown)
    {
        itemHitCombo++;
        if (!wasPerfectlyThrown)
            score += itemHitCombo;
        else
        {
            // Code for perfect throws. Not entirely decided on yet.
            score += itemHitCombo * 2;
        }
        scoreText.text = "Score: " + score;
    }

    public void ResetMultiplier()
    {
        itemHitCombo = 0;
    }

    //public void IncrementScore(CatchType catchType)
    //{
    //    receivedPoints = 0;
    //    switch (catchType)
    //    {
    //        case CatchType.Perfect:
    //            receivedPoints = perfectCatchPoints;
    //            break;
    //        case CatchType.Normal:
    //            receivedPoints = normalCatchPoints;
    //            break;
    //    }
    //    ApplyMultiplier();
    //    score += receivedPoints;
    //    scoreText.text = "Score: " + score;
    //}

    //private void ApplyMultiplier()
    //{
    //    // todo implement multiplier
    //}

    public void DroppedBall()
    {
        //leftHandParticle.Play();
        //rightHandParticle.Play();

        SceneController.IsPlaying = false;

        // The BallController will delete and respawn the balls after this function call.
    }
}
