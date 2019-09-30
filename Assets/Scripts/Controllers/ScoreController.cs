using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text scoreText;
    [SerializeField]
    private int score;
    private int receivedPoints;
    [SerializeField]
    private int itemHitCombo = 1;

    public bool MultiplierDevilDealActivated { get; set; }

    public enum CatchType { Normal, Perfect, Failed }

    public void IncrementScore(bool wasPerfectlyThrown)
    {
        if (!wasPerfectlyThrown)
        {
            // when multiplier devil deal is activated, only build up multiplier when perfect throw.
            if (!MultiplierDevilDealActivated)
                itemHitCombo++;
            score += itemHitCombo;
        }
        else
        {
            // Code for perfect throws. Not entirely decided on yet.
            score += itemHitCombo * 2;
            itemHitCombo++;
        }
        scoreText.text = "Score: " + score;
    }

    public void ResetMultiplier()
    {
        itemHitCombo = 1;
    }

    public void SaveScore(int level)
    {
        string key = "HighScoreLevel" + level;

        if (!PlayerPrefs.HasKey(key))
            PlayerPrefs.SetInt(key, 0);

        int currentHighScore = PlayerPrefs.GetInt(key);
        // you got a new highscore!
        if (score > currentHighScore)
            PlayerPrefs.SetInt(key, score);
    }

}
