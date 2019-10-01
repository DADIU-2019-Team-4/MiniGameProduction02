using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private int levelNumber;

    [SerializeField]
    private GameObject levelFailedText;

    [SerializeField]
    private GameObject levelCompletedText;

    public bool IsPlaying { get; set; }

    private ScoreController ScoreController;

    private void Awake()
    {
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
        levelFailedText.SetActive(true);
        Time.timeScale = 0;
        IsPlaying = false; // Stops background rotation
    }

    public void LevelCompleted()
    {
        AkSoundEngine.PostEvent("LevelCompleted_event", gameObject);
        levelCompletedText.SetActive(true);
        Time.timeScale = 0;
        IsPlaying = false; // Stops background rotation
        // todo save score
        //ScoreController.SaveScore(levelNumber);
    }

    public void GoToLevelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }
}
