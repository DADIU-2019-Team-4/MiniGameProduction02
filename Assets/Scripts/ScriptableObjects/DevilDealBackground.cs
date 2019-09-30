using UnityEngine;

[CreateAssetMenu(fileName = "Devil Deal Background", menuName = "ScriptableObjects/DevilDealBackground")]
public class DevilDealBackground : DevilDeal
{
    public float rotationSpeedMultiplier;

    public override void ApplyDevilDeal()
    {
        RotateBackground rotateBackground = FindObjectOfType<RotateBackground>();
        rotateBackground.RotationSpeed *= rotationSpeedMultiplier;
    }
}
