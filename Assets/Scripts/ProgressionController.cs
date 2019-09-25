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

    private int totalSteps;
    private int currentSteps;

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

    private SceneController sceneController;

    public enum CatchType { normalCatch, perfectCatch, FailedCatch }

    public enum PartType { Green, Yellow, Red }

    public PartType currentPartType;
    private int previousFillNumber;

    private void Awake()
    {
        sceneController = FindObjectOfType<SceneController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // setting up values
        totalSteps = totalParts * stepsPerPart;
        degreesPerStep = arrowEndValueInDegrees / totalSteps;
        stepsPerColor = totalSteps / totalColors;
        currentSteps = arrowStartInSteps;
        previousFillNumber = Mathf.FloorToInt((float)currentSteps / stepsPerPart);

        // setting up parts and arrow
        UpdateParts();
        UpdateArrow(currentSteps);    
        DeterminePartType();
    }

    private void DeterminePartType()
    {
        int partAmount = Mathf.FloorToInt((float) currentSteps / stepsPerColor);
        if (partAmount == 0)
            currentPartType = PartType.Red;
        else if (partAmount == 1)
            currentPartType = PartType.Yellow;
        else if (partAmount == 2)
            currentPartType = PartType.Green;
    }

    private void UpdateArrow(int value)
    {
        arrow.transform.eulerAngles = new Vector3(0, 0, value * degreesPerStep);
  
    }

    private void UpdateParts()
    {
        int partsToFill = Mathf.FloorToInt((float)currentSteps / stepsPerPart);
        if (partsToFill > previousFillNumber)
        {
            AkSoundEngine.PostEvent("FillSound_event" + partsToFill, gameObject);
        }
        if(partsToFill < previousFillNumber && currentSteps!=0)
        {
            AkSoundEngine.PostEvent("UnfillSound_event" + partsToFill, gameObject);
        }
        filling.fillAmount = (float)partsToFill / totalParts;
        previousFillNumber = partsToFill;
    }

    public void UpdateProgression(CatchType catchType)
    {
        switch (catchType)
        {
            case CatchType.normalCatch:
                currentSteps += normalCatchStep;
                if (currentSteps > totalSteps)
                    currentSteps = totalSteps;
                break;
            case CatchType.perfectCatch:
                AkSoundEngine.PostEvent("PerfectCatch_event", gameObject);
                currentSteps += perfectCatchStep;
                if (currentSteps > totalSteps)
                        currentSteps = totalSteps;
                break;
            case CatchType.FailedCatch:
                currentSteps += failStep;
                if (currentSteps <= 0)
                {
                    currentSteps = 0;
                    sceneController.LevelFailed();
                }
                break;
            default:
                break;
        }
        UpdateParts();
        UpdateArrow(currentSteps);
        DeterminePartType();
    }
}
