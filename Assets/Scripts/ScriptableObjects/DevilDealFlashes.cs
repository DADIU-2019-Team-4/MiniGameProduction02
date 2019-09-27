using UnityEngine;

[CreateAssetMenu(fileName = "Devil Deal Flashes", menuName = "ScriptableObjects/DevilDealFlashes")]
public class DevilDealFlashes : DevilDeal
{
    public float interval;

    public override void ApplyDevilDeal()
    {
        SinisterFlashes sinisterFlashes = FindObjectOfType<SinisterFlashes>();
        sinisterFlashes.Interval = interval;
        sinisterFlashes.StartDevilDealFlashing();
    }
}
