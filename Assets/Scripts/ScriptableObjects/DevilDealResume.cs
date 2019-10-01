using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilDealResume : StateMachineBehaviour
{
    private DevilDealController DevilDealController;

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        DevilDealController = FindObjectOfType<DevilDealController>();

        // Continue the game after the animation is over
        DevilDealController.ContinueAfterDevilDealPanel();
    }
}
