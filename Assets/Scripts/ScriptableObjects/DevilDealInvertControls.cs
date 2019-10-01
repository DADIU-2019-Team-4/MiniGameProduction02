using UnityEngine;

[CreateAssetMenu(fileName = "Devil Deal Invert Controls", menuName = "ScriptableObjects/DevilDealInvertControls")]
public class DevilDealInvertControls : DevilDeal
{
    public override void ApplyDevilDeal()
    {
        InputController inputController = FindObjectOfType<InputController>();
        inputController.InvertControls = true;

        BallController ballController = FindObjectOfType<BallController>();
        ballController.IsAlwaysPerfectCatch = true;
    }
}
