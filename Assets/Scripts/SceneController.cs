using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject levelFailedText;

    [SerializeField]
    private GameObject levelCompletedText;

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private float timerValue = 60f;

    public bool IsPlaying { get; set; }

    private int prevSecond = 6;

    private void Update()
    {
        if (IsPlaying)
            UpdateTimer();
    }

    private void UpdateTimer()
    {
        if (Time.timeScale != 0)
        {
            timerValue -= Time.deltaTime * (1 / Time.timeScale);
            //timerValue -= Time.deltaTime;
            UpdateTimerText();

            if (timerValue <= 0)
            {
                timerValue = 0;
                UpdateTimerText();
                LevelCompleted();
            }
        }
    }

    private void UpdateTimerText()
    {
        string minutes = Mathf.Floor(timerValue / 60).ToString("0");
        string seconds = (timerValue % 60).ToString("00");
        timerText.text = $"{minutes}:{seconds}";
        PlayTickSound(int.Parse(minutes),int.Parse(seconds));

    }

    private void PlayTickSound(int minutes, int seconds)
    {
        if (minutes == 0 && seconds <= 5 && prevSecond>seconds)
        {
            AkSoundEngine.PostEvent("TimerSound_event" + seconds,gameObject);
            prevSecond = seconds;
        }
    }

    public void SceneReset()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelFailed()
    {
        AkSoundEngine.PostEvent("FailSound_event", gameObject);
        levelFailedText.SetActive(true);
        Time.timeScale = 0;
    }

    public void LevelCompleted()
    {
        AkSoundEngine.PostEvent("LevelCompleted_event", gameObject);
        levelCompletedText.SetActive(true);
        Time.timeScale = 0;
    }

    public void GoToLevelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }
}
