using UnityEngine;

[CreateAssetMenu(fileName = "Devil Deal Throw Ring", menuName = "ScriptableObjects/DevilDealThrowRing")]
public class DevilDealThrowRing : DevilDeal
{
    public float sizeMultiplier;

    public override void ApplyDevilDeal()
    {
        Hand[] hands = FindObjectsOfType<Hand>();
        foreach (Hand hand in hands)
        {
            hand.gameObject.transform.localScale *= sizeMultiplier;
        }
    }
}
