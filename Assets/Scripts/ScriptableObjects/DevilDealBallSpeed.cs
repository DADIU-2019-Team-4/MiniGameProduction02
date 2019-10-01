using UnityEngine;

[CreateAssetMenu(fileName = "Devil Deal Ball Speed", menuName = "ScriptableObjects/DevilDealBallSpeed")]
public class DevilDealBallSpeed : DevilDeal
{
    public float speedMultiplier;

    public override void ApplyDevilDeal()
    {
        // using time scale for now
        BallController ballController = FindObjectOfType<BallController>();
        ballController.TimeScale *= speedMultiplier;
    }
}
