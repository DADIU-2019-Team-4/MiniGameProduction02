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

    private SceneController SceneController;

    public enum CrowdHappiness { Angry, Neutral, Happy }

    public CrowdHappiness CurrentCrowdHappiness;

    private int prevPartsToFill;
    private bool startOfLevel;

    private void Awake()
    {
        SceneController = FindObjectOfType<SceneController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // setting up values
        totalSteps = totalParts * stepsPerPart;
        degreesPerStep = arrowEndValueInDegrees / totalSteps;
        stepsPerColor = totalSteps / totalColors;
        currentSteps = arrowStartInSteps;
        prevPartsToFill = 0;
        startOfLevel = true;

        // setting up parts and arrow
        UpdateParts();
        UpdateArrow(currentSteps);    
        DeterminePartType();
        startOfLevel = false;
    }

    public void UpdateProgression(ScoreController.CatchType catchType)
    {
        switch (catchType)
        {

            case ScoreController.CatchType.Normal:
                currentSteps += normalCatchStep;
                if (currentSteps > totalSteps)
                    currentSteps = totalSteps;
                break;

            case ScoreController.CatchType.Perfect:
                AkSoundEngine.PostEvent("PerfectCatch_event", gameObject);
                currentSteps += perfectCatchStep;
                if (currentSteps > totalSteps)
                    currentSteps = totalSteps;
                break;

            case ScoreController.CatchType.Failed:
                currentSteps += failStep;

                if (currentSteps <= 0)
                {
                    currentSteps = 0;
                    SceneController.LevelFailed();
                }
                break;
        }

        UpdateParts();
        UpdateArrow(currentSteps);
        DeterminePartType();
    }

    private void DeterminePartType()
    {
        // You can make (ab)use enumerations like this too:
        CurrentCrowdHappiness = (CrowdHappiness) Mathf.FloorToInt((float) currentSteps / stepsPerColor);
        // Just make sure the integer calculated matches the sequence of items in the enumeration.

        //int partAmount = Mathf.FloorToInt((float) currentSteps / stepsPerColor);
        //if (partAmount == 0)
        //    currentPartType = PartType.Red;
        //else if (partAmount == 1)
        //    currentPartType = PartType.Yellow;
        //else if (partAmount == 2)
        //    currentPartType = PartType.Green;
    }

    private void UpdateArrow(int value)
    {
        arrow.transform.eulerAngles = new Vector3(0, 0, value * degreesPerStep);
        if(!startOfLevel)
            AkSoundEngine.PostEvent("ClockArrowSound_event", gameObject);
    }

    private void UpdateParts()
    {
        int partsToFill = Mathf.FloorToInt((float)currentSteps / stepsPerPart);
        if (prevPartsToFill == 0)
        {
            prevPartsToFill = partsToFill;
        }
        if (prevPartsToFill < partsToFill)
        {
            AkSoundEngine.PostEvent("FillSound_event" + partsToFill, gameObject);
            prevPartsToFill = partsToFill;
        }
        else if(prevPartsToFill > partsToFill)
        {
            AkSoundEngine.PostEvent("UnfillSound_event" + partsToFill, gameObject);
            prevPartsToFill = partsToFill;
        }
        filling.fillAmount = (float)partsToFill / totalParts;
    }
}
