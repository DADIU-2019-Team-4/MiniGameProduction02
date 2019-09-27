using UnityEngine;

[CreateAssetMenu(fileName = "Devil Deal Background", menuName = "ScriptableObjects/DevilDealBackground")]
public class DevilDealBackground : DevilDeal
{
    public float rotationSpeed;

    public override void ApplyDevilDeal()
    {
        ClockController clockController = FindObjectOfType<ClockController>();
        clockController.BackgroundRotationSpeed = rotationSpeed;
    }
}
