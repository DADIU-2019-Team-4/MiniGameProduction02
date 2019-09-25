using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private ProgressionController progressionController;

    public Text scoreText;
    private int score;
    private int receivedPoints;

    public Text multiplierText;

    [SerializeField]
    private int normalCatchPoints = 1;
    [SerializeField]
    private int perfectCatchPoints = 3;

    [SerializeField]
    private int neutralMultiplier = 2;

    [SerializeField]
    private int happyMultiplier = 4;

    public enum CatchType { normalCatch, perfectCatch }

    private void Awake()
    {
        progressionController = FindObjectOfType<ProgressionController>();
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
            case CatchType.perfectCatch:
                receivedPoints = perfectCatchPoints;
                break;
            case CatchType.normalCatch:
                receivedPoints = normalCatchPoints;
                break;
        }

        ApplyMultiplier();

        score += receivedPoints;
        scoreText.text = "Score: " + score;
    }

    private void ApplyMultiplier()
    {
        switch (progressionController.CurrentCrowdHappiness)
        {
            case ProgressionController.CrowdHappiness.Happy:
                receivedPoints *= happyMultiplier;
                multiplierText.text = happyMultiplier + "x";
                break;
            case ProgressionController.CrowdHappiness.Neutral:
                receivedPoints *= neutralMultiplier;
                multiplierText.text = neutralMultiplier + "x";
                break;
        }
    }
}
