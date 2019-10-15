using UnityEngine;

[CreateAssetMenu(fileName = "Devil Deal Lives", menuName = "ScriptableObjects/DevilDealLives")]
public class DevilDealLives : DevilDeal
{
    public override void ApplyDevilDeal()
    {
        LifeManager lifeManager = FindObjectOfType<LifeManager>();
        lifeManager.maxLives--;
        lifeManager.ResetLives();
    }
}
