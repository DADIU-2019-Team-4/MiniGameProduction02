using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private SceneController SceneController;

    public Text scoreText;
    private int score;
    private int receivedPoints;

    [SerializeField]
    private int normalCatchPoints = 1;
    [SerializeField]
    private int perfectCatchPoints = 3;

    public enum CatchType { Normal, Perfect, Failed }

    private void Awake()
    {
        SceneController = FindObjectOfType<SceneController>();
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
        // todo implement multiplier
    }
}
