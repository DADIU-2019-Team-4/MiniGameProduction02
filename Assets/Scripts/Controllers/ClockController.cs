using UnityEngine;
using UnityEngine.UI;

public class ClockController : MonoBehaviour
{
    private SceneController sceneController;

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private Image filling;

    [SerializeField]
    private Image arrow;

    // delete this and modify everything related if digital clock is chosen (it's not needed for analog clock)
    private float maxTimerValue = 60f;

    public float TimerValue = 30f;

    public GameObject Background;

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
        float timeRatio = CurrentTimerValue / 60;
        float value = arrowEndValueInDegrees - arrowEndValueInDegrees * timeRatio;
        arrow.transform.eulerAngles = new Vector3(0, 0, value);
        RotateScenery(value);
    }

    private void RotateScenery(float value)
    {
        if (Background != null)
            Background.GetComponent<Transform>().eulerAngles = new Vector3(0, value, 0);
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
        float fillingValue = CurrentTimerValue / 60;
        filling.fillAmount = fillingValue;

        // update the digital clock
        string minutes = Mathf.Floor(CurrentTimerValue / 60).ToString("00");
        string seconds = (CurrentTimerValue % 60).ToString("00");

        timerText.text = $"{minutes}:{seconds}";
    }
}
