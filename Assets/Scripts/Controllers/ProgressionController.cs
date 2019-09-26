using UnityEngine;
using UnityEngine.UI;

public class ProgressionController : MonoBehaviour
{
    [SerializeField]
    private Image filling;

    [SerializeField]
    private Image arrow;

    // green, yellow, red
    private const int totalColors = 3;
    private int stepsPerColor;

    [SerializeField]
    private int totalParts = 6;

    [SerializeField]
    private int stepsPerPart = 3;

    public int TotalSteps { get; set; }
    public int CurrentSteps { get; set; }

    private float degreesPerStep;

    [SerializeField]
    private float arrowEndValueInDegrees = -180;

    [SerializeField]
    private int arrowStartInSteps = 7;

    [SerializeField]
    private int normalCatchStep = 1;
    [SerializeField]
    private int perfectCatchStep = 2;
    [SerializeField]
    private int failStep = -2;

    private bool devilDealActivated;

    private SceneController SceneController;
    private DevilDealController DevilDealController;

    public enum CrowdHappiness { Angry, Neutral, Happy }

    public CrowdHappiness CurrentCrowdHappiness;

    private void Awake()
    {
        SceneController = FindObjectOfType<SceneController>();
        DevilDealController = FindObjectOfType<DevilDealController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // setting up values
        TotalSteps = totalParts * stepsPerPart;
        degreesPerStep = arrowEndValueInDegrees / TotalSteps;
        stepsPerColor = TotalSteps / totalColors;
        CurrentSteps = arrowStartInSteps;

        // setting up parts and arrow
        UpdateParts();
        UpdateArrow();    
        DeterminePartType();
    }

    public void UpdateProgression(ScoreController.CatchType catchType)
    {
        switch (catchType)
        {

            case ScoreController.CatchType.Normal:
                CurrentSteps += normalCatchStep;
                if (CurrentSteps > TotalSteps)
                    CurrentSteps = TotalSteps;
                break;

            case ScoreController.CatchType.Perfect:
                CurrentSteps += perfectCatchStep;
                if (CurrentSteps > TotalSteps)
                    CurrentSteps = TotalSteps;
                break;

            case ScoreController.CatchType.Failed:
                CurrentSteps += failStep;
                if (CurrentSteps <= 0)
                {
                    CurrentSteps = 0;
                    SceneController.LevelFailed();
                }
                break;
        }

        UpdateParts();
        UpdateArrow();
    }

    private void DeterminePartType()
    {
        // You can make (ab)use enumerations like this too:
        CurrentCrowdHappiness = (CrowdHappiness) Mathf.FloorToInt((float) CurrentSteps / stepsPerColor);
        // Just make sure the integer calculated matches the sequence of items in the enumeration.

        //int partAmount = Mathf.FloorToInt((float) CurrentSteps / stepsPerColor);
        //if (partAmount == 0)
        //    currentPartType = PartType.Red;
        //else if (partAmount == 1)
        //    currentPartType = PartType.Yellow;
        //else if (partAmount == 2)
        //    currentPartType = PartType.Green;
    }

    public void UpdateArrow()
    {
        arrow.transform.eulerAngles = new Vector3(0, 0, CurrentSteps * degreesPerStep);
    }

    public void UpdateParts()
    {
        int partsToFill = Mathf.FloorToInt((float)CurrentSteps / stepsPerPart);
        filling.fillAmount = (float)partsToFill / totalParts;

        DeterminePartType();
        CheckDevilDealActivation(partsToFill);
    }

    private void CheckDevilDealActivation(int partAmount)
    {
        if (partAmount < 1 && !devilDealActivated)
        {
            devilDealActivated = true;
            DevilDealController.ActivateDevilDealPanel();
        }

        if (partAmount >= 1 && devilDealActivated)
            devilDealActivated = false;
    }
}
