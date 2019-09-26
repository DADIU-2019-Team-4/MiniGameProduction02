using UnityEngine;
using UnityEngine.UI;

public class ClockController : MonoBehaviour
{
    private SceneController sceneController;

    [SerializeField]
    private Image filling;

    [SerializeField]
    private Image arrow;

    public float TimerValue { get; set; } = 60f;

    [HideInInspector]
    public float CurrentTimerValue { get; set; }

    private float arrowDegreePerSecond = 6;
    private float arrowEndValueInDegrees;

    private void Awake()
    {
        sceneController = FindObjectOfType<SceneController>();
    }

    private void Start()
    {
        arrowEndValueInDegrees = arrowDegreePerSecond * TimerValue;
        CurrentTimerValue = TimerValue;
        UpdateArrow();
        filling.fillAmount = 1;
    }

    private void Update()
    {
        if (sceneController.IsPlaying)
            UpdateTimer();
    }

    private void UpdateArrow()
    {
        float timeRatio = CurrentTimerValue / TimerValue;
        float value = arrowEndValueInDegrees - arrowEndValueInDegrees * timeRatio;
        arrow.transform.eulerAngles = new Vector3(0, 0, value);
    }

    private void UpdateTimer()
    {
        if (Time.timeScale != 0)
        {
            CurrentTimerValue -= Time.deltaTime * (1 / Time.timeScale);

            if (CurrentTimerValue <= 0)
                sceneController.LevelCompleted();

            UpdateClock();
            UpdateArrow();
        }
    }

    private void UpdateClock()
    {
        float fillingValue = CurrentTimerValue / TimerValue;
        filling.fillAmount = fillingValue;
    }
}
