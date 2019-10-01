using UnityEngine;

[CreateAssetMenu(fileName = "Devil Deal Cut Off Hand", menuName = "ScriptableObjects/DevilDealCutOffHand")]
public class DevilDealCutOffHand : DevilDeal
{
    public override void ApplyDevilDeal()
    {
        InputController inputController = FindObjectOfType<InputController>();
        inputController.HandCutOff = true;
    }
}
