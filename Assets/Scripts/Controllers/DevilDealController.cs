using System.Collections.Generic;
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
    private Image negativeDealImage;

    [SerializeField]
    private GameObject devilSkullSpawnPoint;
    [SerializeField]
    private GameObject devilSkull;

    [SerializeField]
    private List<DevilDeal> devilDeals = new List<DevilDeal>();
    private DevilDeal chosenNegativeDevilDeal;

    private int acceptedDevilDealsCount;
    private List<DevilDeal> acceptedDevilDeals = new List<DevilDeal>();
    [SerializeField]
    private int maxAcceptedDevilDeals = 10;
    private int acceptedNegativeDealsCount;

    private bool applyNegativeEffect;

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
        // todo play animation

        // todo insert some check to only activate this when animation is done playing
        devilDealCanvas.SetActive(true);
        negativeDealImage.gameObject.SetActive(false);
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
        if (acceptedDevilDealsCount >= maxAcceptedDevilDeals)
        {
            //todo insert method to ending by too much devil deals
            return;
        }

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
        negativeDealImage.sprite = chosenNegativeDevilDeal.image;
        negativeDealImage.gameObject.SetActive(true);
    }

    private void ApplyNegativeEffect()
    {
        // todo should save it for long term
        acceptedDevilDeals.Add(chosenNegativeDevilDeal);

        // todo save this value for long term
        acceptedNegativeDealsCount++;

        chosenNegativeDevilDeal.ApplyDevilDeal();
    }

    public void DeclineDevilDeal()
    {
        devilDealCanvas.SetActive(false);
        Time.timeScale = BallController.TimeScale;
    }

}
