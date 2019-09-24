using UnityEngine;
using UnityEngine.UI;

public class ProgressionController : MonoBehaviour
{
    [SerializeField]
    private Image filling;

    [SerializeField]
    private Image arrow;

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
    private int failStep = -1;

    private SceneController sceneController;

    public enum CatchType { normalCatch, perfectCatch, FailedCatch }

    private void Awake()
    {
        sceneController = FindObjectOfType<SceneController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        totalSteps = totalParts * stepsPerPart;
        degreesPerStep = arrowEndValueInDegrees / totalSteps;
        currentSteps = arrowStartInSteps;

        int partsToFill = Mathf.FloorToInt((float)arrowStartInSteps / stepsPerPart);
        filling.fillAmount = (float)partsToFill/totalParts;

        arrow.transform.Rotate(0, 0, arrowStartInSteps * degreesPerStep);
    }

    private void Update()
    {
        if (filling.fillAmount <= 0)
            sceneController.LevelFailed();
    }

    public void UpdateProgressionArrow(CatchType catchType)
    {
        switch (catchType)
        {
            case CatchType.normalCatch:

                break;
            case CatchType.perfectCatch:

                break;
            case CatchType.FailedCatch:

                break;
            default:
                break;
        }
    }
}
