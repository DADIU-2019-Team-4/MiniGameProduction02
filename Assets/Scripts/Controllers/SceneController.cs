using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    private Endings endings;
    private ScoreController ScoreController;

    [SerializeField]
    private GameObject levelFailed;

    [SerializeField]
    private GameObject levelCompleted;

    [SerializeField]
    private Text scoreText;

    public bool IsPlaying { get; set; }

    public bool GameEnded { get; set; }

    private void Awake()
    {
        endings = FindObjectOfType<Endings>();
        ScoreController = FindObjectOfType<ScoreController>();
    }

    public void ResetScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelFailed()
    {
        AkSoundEngine.PostEvent("FailSound_event", gameObject);
        Time.timeScale = 0;
        IsPlaying = false; // Stops background rotation

        endings.CheckGameFailedEnding();
        GameEnded = endings.GameEnded;

        if (!GameEnded)
            levelFailed.SetActive(true);
    }

    public void LevelCompleted()
    {
        AkSoundEngine.PostEvent("LevelCompleted_event", gameObject);
        Time.timeScale = 0;
        IsPlaying = false; // Stops background rotation

        endings.CheckGameCompletedEnding();
        GameEnded = endings.GameEnded;

        if (!GameEnded)
        {
            levelCompleted.SetActive(true);
            scoreText.text = ScoreController.publicScore.ToString();
        }
    }

    public void GoToLevelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }
}
