using UnityEngine;
using UnityEngine.UI;

public class ClockController : MonoBehaviour
{
    private SceneController sceneController;

    [SerializeField]
    private Image filling;

    [SerializeField]
    private Image arrow;

    [SerializeField]
    private float timerValue = 60f;

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
        filling.fillAmount = 1;
    }

    private void Update()
    {
        if (sceneController.IsPlaying)
            UpdateTimer();
    }

    private void UpdateArrow()
    {
        float timeRatio = currentTimerValue / timerValue;
        float value = arrowEndValueInDegrees - arrowEndValueInDegrees * timeRatio;
        arrow.transform.eulerAngles = new Vector3(0, 0, value);
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
        float fillingValue = currentTimerValue / timerValue;
        filling.fillAmount = fillingValue;
    }
}
