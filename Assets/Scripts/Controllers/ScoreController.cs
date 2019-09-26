using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private ProgressionController ProgressionController;
    private SceneController SceneController;

    public Text scoreText;
    private int score;
    private int receivedPoints;

    public Text multiplierText;

    [SerializeField]
    private int normalCatchPoints = 1;
    [SerializeField]
    private int perfectCatchPoints = 3;

    [SerializeField]
    private int angryMultiplier = 1;

    [SerializeField]
    private int neutralMultiplier = 2;

    [SerializeField]
    private int happyMultiplier = 4;

    public enum CatchType { Normal, Perfect, Failed }

    private void Awake()
    {
        ProgressionController = FindObjectOfType<ProgressionController>();
        SceneController = FindObjectOfType<SceneController>();
    }

    private void Start()
    {
        ApplyMultiplier();
    }

    public void IncrementScore(CatchType catchType)
    {
        receivedPoints = 0;
        switch (catchType)
        {
            case CatchType.Perfect:
                receivedPoints = perfectCatchPoints;
                break;
            case CatchType.Normal:
                receivedPoints = normalCatchPoints;
                break;
        }

        ApplyMultiplier();

        score += receivedPoints;
        scoreText.text = "Score: " + score;
    }

    private void ApplyMultiplier()
    {
        switch (ProgressionController.CurrentCrowdHappiness)
        {
            case ProgressionController.CrowdHappiness.Happy:
                receivedPoints *= happyMultiplier;
                multiplierText.text = happyMultiplier + "x";
                break;
            case ProgressionController.CrowdHappiness.Neutral:
                receivedPoints *= neutralMultiplier;
                multiplierText.text = neutralMultiplier + "x";
                break;
            case ProgressionController.CrowdHappiness.Angry:
                receivedPoints *= angryMultiplier;
                multiplierText.text = neutralMultiplier + "x";
                break;
        }
    }

    public void DroppedBall()
    {
        //leftHandParticle.Play();
        //rightHandParticle.Play();

        ProgressionController.UpdateProgression(CatchType.Failed);
        SceneController.IsPlaying = false;
        // The BallController will delete and respawn the balls after this function call.
    }
}
