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

}
