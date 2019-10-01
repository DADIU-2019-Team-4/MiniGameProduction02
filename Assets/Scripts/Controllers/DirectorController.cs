using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorController : MonoBehaviour
{
    private Animator DirectorAnimator;

    // Start is called before the first frame update
    void Start()
    {
        DirectorAnimator = gameObject.GetComponent<Animator>();
    }

    public void PlayIntroAnimation()
    {
        if (DirectorAnimator)
            DirectorAnimator.SetTrigger("playIntro");
    }

    public void PlayDevilDealAnimation()
    {
        if (DirectorAnimator)
            DirectorAnimator.SetTrigger("startDevilDeal");
    }

    public void ContinueDevilDealAnimation()
    {
        if (DirectorAnimator)
            DirectorAnimator.SetTrigger("continueDevilDeal");
    }
}
