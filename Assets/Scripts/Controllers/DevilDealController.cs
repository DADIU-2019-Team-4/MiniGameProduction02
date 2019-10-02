using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DevilDealController : MonoBehaviour
{
    private BallController BallController;
    private DirectorController DirectorController;
    private LifeManager LifeManager;
    private SceneController SceneController;
    private SinisterFlashes SinisterFlashes;
    private MenuController MenuController;

    [SerializeField]
    private GameObject devilDealCanvas;

    private GameObject ChoicesEnglish;
    private GameObject ChoicesDanish;
    private GameObject TitleEnglish;
    private GameObject TitleDanish;

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
    public bool IsDevilDealTime { get; set; }

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
        MenuController = FindObjectOfType<MenuController>();
    }

    private void Start()
    {
        TitleEnglish = GameObject.Find("Background_English");
        TitleDanish = GameObject.Find("Background_Danish");
        ChoicesEnglish = GameObject.Find("Choice_English");
        ChoicesDanish = GameObject.Find("Choice_Danish");
        if (TitleEnglish != null)
            ChangeLanguage();

        devilDealCanvas.SetActive(false);

        MaxDevilDeals = devilDeals.Count;
        IsDevilDealTime = false;

        imageWidth = devilSkull.GetComponent<RectTransform>().rect.width;
        SpawnDevilSkulls();

        ActivateDevilDeals();

        if (MenuController.ChangeLanguageEvent == null)
            MenuController.ChangeLanguageEvent = new UnityEvent();

        MenuController.ChangeLanguageEvent.AddListener(ChangeLanguage);
    }

    private void ChangeLanguage()
    {
        if (MenuController.language == "English")
        {
            TitleEnglish.SetActive(true);
            TitleDanish.SetActive(false);
            ChoicesEnglish.SetActive(true);
            ChoicesDanish.SetActive(false);
        }
        else if (MenuController.language == "Danish")
        {
            TitleEnglish.SetActive(false);
            TitleDanish.SetActive(true);
            ChoicesEnglish.SetActive(false);
            ChoicesDanish.SetActive(true);
        }
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
        IsDevilDealTime = true;

        if (FindObjectOfType<LastTutorialManager>() != null)
            if (FindObjectOfType<LastTutorialManager>()._previousTutorialStage == 4)
            {
                FindObjectOfType<LastTutorialManager>().EnableTutorialUI();
                return;
            }

        // Trigger the Animation. After it is finished, it will call ContinueAfterDevilDealPanel()
        if (DirectorController != null)
            DirectorController.PlayDDIntroAnimation();
        else
            ContinueAfterDevilDealPanel();

        // Remove remaining Balls and stop new ones from spawning
        BallController.Stop();
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

    private void ApplyPositiveEffect()
    {
        LifeManager.ResetLives();
    }

    private void ChooseNegativeDevilDeal()
    {
        chosenNegativeDevilDeal = devilDeals[AcceptedNegativeDealsCount];

        if (MenuController.language == "English")
            descriptionText.text = chosenNegativeDevilDeal.dealDescriptionEnglish;
        else if (MenuController.language == "Danish")
            descriptionText.text = chosenNegativeDevilDeal.dealDescriptionDanish;
    }

    public void DeclineDevilDeal()
    {
        devilDealCanvas.SetActive(false);
        AkSoundEngine.PostEvent("DDNegative_event", gameObject);
        ContinuePlaying();
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

    public void ContinuePlaying()
    {
        // Trigger the Animation (don't wait for it to finish)
        if (DirectorController != null)
            DirectorController.PlayDDOutroAnimation();
        Time.timeScale = BallController.TimeScale;
        BallController.Restart();
        SceneController.IsPlaying = true;
        IsDevilDealTime = false;
    }
}
