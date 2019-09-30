using UnityEngine;

[CreateAssetMenu(fileName = "Devil Deal Background And Flashes", menuName = "ScriptableObjects/DevilDealBackgroundAndFlashes")]
public class DevilDealBackgroundAndFlashes : DevilDeal
{
    public float rotationSpeedMultiplier;

    public float interval;

    public override void ApplyDevilDeal()
    {
        RotateBackground rotateBackground = FindObjectOfType<RotateBackground>();
        rotateBackground.RotationSpeed *= rotationSpeedMultiplier;

        SinisterFlashes sinisterFlashes = FindObjectOfType<SinisterFlashes>();
        sinisterFlashes.Interval = interval;
        sinisterFlashes.StartDevilDealFlashing();
    }
}
