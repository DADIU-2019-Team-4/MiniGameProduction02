using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroResume : StateMachineBehaviour
{
    private IntroController IntroController;

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        IntroController = FindObjectOfType<IntroController>();

        // Continue the game after the animation is over
        IntroController.Finish();
    }
}
