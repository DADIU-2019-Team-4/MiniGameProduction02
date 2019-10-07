using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text scoreText;

    public int Score;

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
            Score += itemHitCombo;
        }
        else
        {
            // Code for perfect throws. Not entirely decided on yet.
            Score += itemHitCombo * 2;
            itemHitCombo++;
        }
        scoreText.text = Score.ToString();
    }

    public void ResetMultiplier()
    {
        itemHitCombo = 1;
    }

}
