using UnityEngine;

[CreateAssetMenu(fileName = "Devil Deal Perfect Multiplier", menuName = "ScriptableObjects/DevilDealPerfectMultiplier")]
public class DevilDealPerfectMultiplier : DevilDeal
{
    public override void ApplyDevilDeal()
    {
        ScoreController scoreController = FindObjectOfType<ScoreController>();
        scoreController.MultiplierDevilDealActivated = true;
    }
}
