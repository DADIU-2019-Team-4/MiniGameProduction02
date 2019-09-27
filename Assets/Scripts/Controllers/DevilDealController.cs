﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevilDealController : MonoBehaviour
{
    private BallController BallController;
    private ProgressionController ProgressionController;

    [SerializeField]
    private GameObject devilDealCanvas;

    [SerializeField]
    private Text descriptionText;

    [SerializeField]
    private GameObject devilSkullSpawnPoint;
    [SerializeField]
    private GameObject devilSkull;

    [SerializeField]
    private List<DevilDeal> devilDeals = new List<DevilDeal>();
    private DevilDeal chosenNegativeDevilDeal;

    private int acceptedDevilDealsCount;
    private int acceptedNegativeDealsCount;

    private bool applyNegativeEffect;
    private bool lastDevilDeal;

    [SerializeField]
    private int countUntilNegativeEffect = 2;
    private int devilSkullCount;
    private float offset = 40;
    private float imageWidth;

    [SerializeField, TextArea(10, 100)]
    private string dealDescription;

    private void Awake()
    {
        BallController = FindObjectOfType<BallController>();
        ProgressionController = FindObjectOfType<ProgressionController>();
    }

    private void Start()
    {
        imageWidth = devilSkull.GetComponent<RectTransform>().rect.width;
        SpawnDevilSkulls();

        ActivateDevilDeals();
    }

    private void ActivateDevilDeals()
    {
        for (int i = 0; i < acceptedNegativeDealsCount; i++)
        {
            chosenNegativeDevilDeal = devilDeals[i];
            chosenNegativeDevilDeal.ApplyDevilDeal();
        }
    }

    private void SpawnDevilSkulls()
    {
        for (int i = 0; i < devilSkullCount; i++)
        {
            GameObject skull = Instantiate(devilSkull, devilSkullSpawnPoint.transform);
            Vector3 skullPos = skull.transform.localPosition;
            float newXPos = skullPos.x + (imageWidth + offset) * i;
            skull.transform.localPosition = new Vector3(newXPos, skullPos.y);
        }
    }

    private void ClearDevilSkulls()
    {
        devilSkullCount = 0;
        Image[] skulls = devilSkullSpawnPoint.GetComponentsInChildren<Image>();
        foreach (Image skull in skulls)
            Destroy(skull.gameObject);
    }

    private void Update()
    {
        if (ProgressionController.ActivateDevilDeal)
        {
            ProgressionController.ActivateDevilDeal = false;

            if (!lastDevilDeal)
                ActivateDevilDealPanel();
        }
    }

    public void ActivateDevilDealPanel()
    {
        // todo play animation and continue when animation is done playing
        devilDealCanvas.SetActive(true);
        descriptionText.text = dealDescription;

        if (acceptedDevilDealsCount > 0 && acceptedDevilDealsCount % countUntilNegativeEffect == 0)
        {
            applyNegativeEffect = true;
            ChooseNegativeDevilDeal();
        }
        Time.timeScale = 0;
    }

    public void AcceptDevilDeal()
    {
        if (applyNegativeEffect)
        {
            ApplyNegativeEffect();
            ClearDevilSkulls();
            applyNegativeEffect = false;
        }
        else
        {
            // todo save this value for long term
            devilSkullCount++;
            SpawnDevilSkulls();
        }

        // todo save this value for long term
        acceptedDevilDealsCount++;

        ApplyPositiveEffect();

        devilDealCanvas.SetActive(false);
        Time.timeScale = BallController.TimeScale;
    }

    private void ApplyPositiveEffect()
    {
        ProgressionController.CurrentSteps = ProgressionController.TotalSteps / 2;
        ProgressionController.UpdateArrow();
        ProgressionController.UpdateParts();
    }

    private void ChooseNegativeDevilDeal()
    {
        chosenNegativeDevilDeal = devilDeals[acceptedNegativeDealsCount];

        descriptionText.text = chosenNegativeDevilDeal.dealDescription;
    }

    private void ApplyNegativeEffect()
    {
        // todo save this value for long term
        acceptedNegativeDealsCount++;

        chosenNegativeDevilDeal.ApplyDevilDeal();


        if (acceptedNegativeDealsCount >= devilDeals.Count)
            lastDevilDeal = true;
    }

    public void DeclineDevilDeal()
    {
        devilDealCanvas.SetActive(false);
        Time.timeScale = BallController.TimeScale;
    }

}
