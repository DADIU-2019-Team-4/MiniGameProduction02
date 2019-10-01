using UnityEngine;

[CreateAssetMenu(fileName = "Devil Deal Ball Speed", menuName = "ScriptableObjects/DevilDealBallSpeed")]
public class DevilDealBallSpeed : DevilDeal
{
    public float speedMultiplier;

    public override void ApplyDevilDeal()
    {
        BallController ballController = FindObjectOfType<BallController>();
        // todo apply speed to ball (waiting for animations or physics)
    }
}
