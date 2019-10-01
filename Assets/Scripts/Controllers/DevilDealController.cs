﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DevilDealController : MonoBehaviour
{
    private BallController BallController;
    private DirectorController DirectorController;
    private LifeManager LifeManager;
    private SceneController SceneController;
    private SinisterFlashes SinisterFlashes;

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

    public int MaxDevilDeals { get; set; }

    private int acceptedDevilDealsCount;
    public int AcceptedNegativeDealsCount { get; set; }

    private bool applyNegativeEffect;
    public bool LastDevilDeal { get; set; }

    private int countUntilNegativeEffect = 1;
    private int devilSkullCount;
    private float offset = 40;
    private float imageWidth;

    [SerializeField, TextArea(10, 100)]
    private string dealDescription;

    [SerializeField]
    private float lengthOfFlash = 0.2f;

    [SerializeField]
    private float maxFlashAlphaValue = 0.8f;

    private void Awake()
    {
        DirectorController = FindObjectOfType<DirectorController>();
        BallController = FindObjectOfType<BallController>();
        LifeManager = FindObjectOfType<LifeManager>();
        SceneController = FindObjectOfType<SceneController>();
        SinisterFlashes = FindObjectOfType<SinisterFlashes>();
    }

    private void Start()
    {
        MaxDevilDeals = devilDeals.Count;

        imageWidth = devilSkull.GetComponent<RectTransform>().rect.width;
        SpawnDevilSkulls();

        ActivateDevilDeals();
    }

    private void ActivateDevilDeals()
    {
        for (int i = 0; i < AcceptedNegativeDealsCount; i++)
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

    public void ActivateDevilDealPanel()
    {
        if (FindObjectOfType<LastTutorialManager>() != null)
            if (FindObjectOfType<LastTutorialManager>()._previousTutorialStage == 4)
            {
                FindObjectOfType<LastTutorialManager>().EnableTutorialUI();
                return;
            }
        // Trigger the Animation. After it is finished, it will call ContinueAfterDevilDealPanel()
        DirectorController.PlayDDIntroAnimation();
    }

    public void ContinueAfterDevilDealPanel()
    {
        // Runs when animation is done playing
        SceneController.IsPlaying = false;
        AkSoundEngine.PostEvent("DDIntro_event", gameObject);

        devilDealCanvas.SetActive(true);
        descriptionText.text = dealDescription;

        if (acceptedDevilDealsCount % countUntilNegativeEffect == 0)
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
            StartCoroutine(ApplyNegativeEffect());
            ClearDevilSkulls();
            applyNegativeEffect = false;
        }
        else
        {
            // todo save this value for long term
            devilSkullCount++;
            SpawnDevilSkulls();
        }
        AkSoundEngine.PostEvent("DDPositive_event", gameObject);
        // todo save this value for long term
        acceptedDevilDealsCount++;

        ApplyPositiveEffect();
        devilDealCanvas.SetActive(false);
        ContinuePlaying();
    }

    public void DeclineDevilDeal()
    {
        devilDealCanvas.SetActive(false);
        AkSoundEngine.PostEvent("DDNegative_event", gameObject);
        ContinuePlaying();
    }

    private void ApplyPositiveEffect()
    {
        LifeManager.ResetLives();
    }

    private IEnumerator ApplyNegativeEffect()
    {
        // todo save this value for long term
        AcceptedNegativeDealsCount++;

        SinisterFlashes.SinisterFlashingImage.gameObject.SetActive(true);
        SinisterFlashes.SinisterFlashingImage.DOFade(maxFlashAlphaValue, lengthOfFlash / 2);
        yield return new WaitForSeconds(lengthOfFlash / 2);

        // todo insert code for changing viola's face to the next stage
        chosenNegativeDevilDeal.ApplyDevilDeal();

        SinisterFlashes.SinisterFlashingImage.DOFade(0, lengthOfFlash / 2);
        yield return new WaitForSeconds(lengthOfFlash / 2);
        SinisterFlashes.SinisterFlashingImage.gameObject.SetActive(false);

        if (AcceptedNegativeDealsCount >= devilDeals.Count)
            LastDevilDeal = true;
    }

    private void ChooseNegativeDevilDeal()
    {
        chosenNegativeDevilDeal = devilDeals[AcceptedNegativeDealsCount];

        descriptionText.text = chosenNegativeDevilDeal.dealDescription;
    }

    public void ContinuePlaying()
    {
        // Trigger the Animation (don't wait for it to finish)
        DirectorController.PlayDDOutroAnimation();
        Time.timeScale = BallController.TimeScale;
        BallController.Restart();
        SceneController.IsPlaying = true;
    }
}
