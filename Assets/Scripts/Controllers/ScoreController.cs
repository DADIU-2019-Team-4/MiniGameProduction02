using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text scoreText;
    private int score;

    private ProgressionController ProgressionController;
    private SceneController SceneController;

    [SerializeField]
    private int normalCatchPoints = 1;
    [SerializeField]
    private int perfectCatchPoints = 3;

    public enum CatchType { Perfect, Normal, Failed }

    private void Awake()
    {
        ProgressionController = FindObjectOfType<ProgressionController>();
        SceneController = FindObjectOfType<SceneController>();
    }

    public void IncrementScore(CatchType catchType)
    {
        if (catchType == CatchType.Perfect)
            score += perfectCatchPoints;
        else if (catchType == CatchType.Normal)
            score += normalCatchPoints;

        scoreText.text = "Score: " + score;
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
