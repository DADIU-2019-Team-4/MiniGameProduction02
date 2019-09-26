using UnityEngine;
using UnityEngine.UI;

public class ClockController : MonoBehaviour
{
    private SceneController sceneController;

    [SerializeField]
    private Image filling;

    [SerializeField]
    private Image arrow;

    private float maxTimerValue = 60f;
    public float TimerValue = 30f;

    [HideInInspector]
    public float CurrentTimerValue { get; set; }

    private float arrowEndValueInDegrees = 360f;

    private void Awake()
    {
        sceneController = FindObjectOfType<SceneController>();
    }

    private void Start()
    {
        CurrentTimerValue = TimerValue;
        UpdateArrow();
        UpdateClock();
    }

    private void Update()
    {
        if (sceneController.IsPlaying)
            UpdateTimer();
    }

    public void UpdateArrow()
    {
        float timeRatio = CurrentTimerValue / maxTimerValue;
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

    public void UpdateClock()
    {
        float fillingValue = CurrentTimerValue / maxTimerValue;
        filling.fillAmount = fillingValue;
    }
}
