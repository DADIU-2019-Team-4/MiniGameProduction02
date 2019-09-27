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

    [SerializeField]
    private float timerValue = 60f;

    public GameObject Background;

    private float currentTimerValue;

    private float arrowEndValueInDegrees = 360;

    private void Awake()
    {
        sceneController = FindObjectOfType<SceneController>();
    }

    private void Start()
    {
        currentTimerValue = timerValue;
        UpdateArrow();
        UpdateClock();
    }

    private void Update()
    {
        if (sceneController.IsPlaying)
            UpdateTimer();
    }

    private void UpdateArrow()
    {
        float timeRatio = currentTimerValue / 60;
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
            currentTimerValue -= Time.deltaTime * (1 / Time.timeScale);

            if (currentTimerValue <= 0)
                sceneController.LevelCompleted();

            UpdateClock();
            UpdateArrow();
        }
    }

    private void UpdateClock()
    {
        // update the analog clock
        float fillingValue = currentTimerValue / 60;
        filling.fillAmount = fillingValue;

        // update the digital clock
        string minutes = Mathf.Floor(currentTimerValue / 60).ToString("00");
        string seconds = (currentTimerValue % 60).ToString("00");

        timerText.text = $"{minutes}:{seconds}";
    }
}
