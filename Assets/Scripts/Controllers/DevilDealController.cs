using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DevilDealController : MonoBehaviour
{
    private BallController BallController;
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

    private int acceptedDevilDealsCount;
    private string acceptedDevilDealsCountKey = "acceptedDevilDealsCount";
    private int acceptedNegativeDealsCount;
    private string acceptedNegativeDealsCountKey = "acceptedNegativeDealsCount";

    private bool applyNegativeEffect;
    public bool LastDevilDeal { get; set; }

    [SerializeField]
    private int countUntilNegativeEffect = 2;
    private int devilSkullCount;
    private string devilSkullCountKey = "devilSkullCount";
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
        BallController = FindObjectOfType<BallController>();
        LifeManager = FindObjectOfType<LifeManager>();
        SceneController = FindObjectOfType<SceneController>();
        SinisterFlashes = FindObjectOfType<SinisterFlashes>();
    }

    private void Start()
    {
        imageWidth = devilSkull.GetComponent<RectTransform>().rect.width;

        InitializeDevilDeals();
        SpawnDevilSkulls();
        ActivateDevilDeals();
    }

    private void InitializeDevilDeals()
    {
        if (!PlayerPrefs.HasKey(acceptedDevilDealsCountKey))
            PlayerPrefs.SetInt(acceptedDevilDealsCountKey, 0);
        if(!PlayerPrefs.HasKey(acceptedNegativeDealsCountKey))
            PlayerPrefs.SetInt(acceptedNegativeDealsCountKey, 0);
        if (!PlayerPrefs.HasKey(devilSkullCountKey))
            PlayerPrefs.SetInt(devilSkullCountKey, 0);

        acceptedDevilDealsCount = PlayerPrefs.GetInt(acceptedDevilDealsCountKey);
        acceptedNegativeDealsCount = PlayerPrefs.GetInt(acceptedNegativeDealsCountKey);
        devilSkullCount = PlayerPrefs.GetInt(devilSkullCountKey);
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

    public void ActivateDevilDealPanel()
    {
        // todo play animation and continue when animation is done playing
        SceneController.IsPlaying = false;

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
            StartCoroutine(ApplyNegativeEffect());
            ClearDevilSkulls();
            applyNegativeEffect = false;
        }
        else
        {
            devilSkullCount++;
            // saves devil skull count
            PlayerPrefs.SetInt(devilSkullCountKey, devilSkullCount);
            SpawnDevilSkulls();
        }

        acceptedDevilDealsCount++;
        // saves total accepted devil deal count
        PlayerPrefs.SetInt(acceptedDevilDealsCountKey, acceptedDevilDealsCount);

        ApplyPositiveEffect();

        devilDealCanvas.SetActive(false);
        Time.timeScale = BallController.TimeScale;

        SceneController.IsPlaying = true;
    }

    private void ApplyPositiveEffect()
    {
        LifeManager.ResetLives();
    }

    private void ChooseNegativeDevilDeal()
    {
        chosenNegativeDevilDeal = devilDeals[acceptedNegativeDealsCount];

        descriptionText.text = chosenNegativeDevilDeal.dealDescription;
    }

    public void DeclineDevilDeal()
    {
        devilDealCanvas.SetActive(false);
        Time.timeScale = BallController.TimeScale;

        SceneController.IsPlaying = true;
    }

    private IEnumerator ApplyNegativeEffect()
    {
        acceptedNegativeDealsCount++;
        // saves total accepted negative devil deal count
        PlayerPrefs.SetInt(acceptedNegativeDealsCountKey, acceptedNegativeDealsCount);

        SinisterFlashes.SinisterFlashingImage.gameObject.SetActive(true);
        SinisterFlashes.SinisterFlashingImage.DOFade(maxFlashAlphaValue, lengthOfFlash / 2);
        yield return new WaitForSeconds(lengthOfFlash / 2);

        // todo insert code for changing viola's face to the next stage
        chosenNegativeDevilDeal.ApplyDevilDeal();

        SinisterFlashes.SinisterFlashingImage.DOFade(0, lengthOfFlash / 2);
        yield return new WaitForSeconds(lengthOfFlash / 2);
        SinisterFlashes.SinisterFlashingImage.gameObject.SetActive(false);

        if (acceptedNegativeDealsCount >= devilDeals.Count)
            LastDevilDeal = true;
    }
}
