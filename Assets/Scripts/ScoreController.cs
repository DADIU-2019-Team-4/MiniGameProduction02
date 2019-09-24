using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text scoreText;
    private int score;

    [SerializeField]
    private int normalCatchPoints = 1;
    [SerializeField]
    private int perfectCatchPoints = 3;

    public enum CatchType { normalCatch, perfectCatch }

    public void IncrementScore(CatchType catchType)
    {
        if (catchType == CatchType.perfectCatch)
            score += perfectCatchPoints;
        else if (catchType == CatchType.normalCatch)
            score += normalCatchPoints;

        scoreText.text = "Score: " + score;
    }
}
