using UnityEngine;
using UnityEngine.UI;

public class ProgressionController : MonoBehaviour
{
    [SerializeField]
    private Image filling;

    [SerializeField]
    private int totalAmount = 6;

    [SerializeField]
    private int startAmount = 3;
    [SerializeField]
    private float depleteSpeed = 0.01f;

    [SerializeField]
    private int normalCatchAmount = 1;
    [SerializeField]
    private int perfectCatchAmount = 1;
    [SerializeField]
    private int failAmount = -1;

    private SceneController sceneController;

    public enum CatchType { normalCatch, perfectCatch, FailedCatch }

    private int streak;
    private int perfectStreak;
    [SerializeField]
    private int requiredStreak = 6;
    [SerializeField]
    private int requiredPerfectStreak = 3;

    private void Awake()
    {
        sceneController = FindObjectOfType<SceneController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        filling.fillAmount = (float)startAmount/totalAmount;
    }

    private void Update()
    {
        //filling.fillAmount -= Time.deltaTime * depleteSpeed;

        if (filling.fillAmount <= 0)
            sceneController.LevelFailed();
    }

    public void UpdateProgression(CatchType catchType)
    {
        switch (catchType)
        {
            case CatchType.normalCatch:
                streak++;
                if (streak >= requiredStreak)
                {
                    filling.fillAmount += (float) normalCatchAmount / totalAmount;
                    streak = 0;
                }
                break;
            case CatchType.perfectCatch:
                perfectStreak++;
                if (perfectStreak >= requiredPerfectStreak)
                {
                    filling.fillAmount += (float) perfectCatchAmount / totalAmount;
                    perfectStreak = 0;
                }
                break;
            case CatchType.FailedCatch:
                streak = 0;
                perfectStreak = 0;
                filling.fillAmount += (float)failAmount/totalAmount;
                break;
            default:
                break;
        }
    }
}
