using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text scoreText;
    private int score;

    private PerfectCatch[] perfectCatch;

    private void Awake()
    {
        perfectCatch = FindObjectsOfType<PerfectCatch>();
    }

    public void IncrementScore()
    {
        foreach (var perfect in perfectCatch)
        {
            if (perfect.perfectCatch)
                score += 2;
        }

        score++;
        scoreText.text = "Score: " + score;
    }
}
