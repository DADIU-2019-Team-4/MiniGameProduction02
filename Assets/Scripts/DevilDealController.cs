using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilDealController : MonoBehaviour
{
    [SerializeField]
    private GameObject devilDealCanvas;

    private GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void ActivateDevilDealPanel()
    {
        // todo insert voice line
        // todo play animation

        // todo insert some check to only do this when animation is done playing
        devilDealCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    private void AcceptedDevilDeal()
    {
        // todo check how many skulls to see if to apply negative effect
        // todo apply positive effect
        // todo save total amount chosen devil deals
        // todo save chosen negative effect
        // todo set current chosen devil deals and set skulls
        devilDealCanvas.SetActive(false);
        Time.timeScale = gameController.timeScale;
    }

    private void DeclinedDevilDeal()
    {
        devilDealCanvas.SetActive(false);
        Time.timeScale = gameController.timeScale;
    }

}
