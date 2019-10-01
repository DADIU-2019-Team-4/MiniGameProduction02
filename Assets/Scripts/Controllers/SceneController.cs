using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private Endings endings;

    [SerializeField]
    private GameObject levelFailedText;

    [SerializeField]
    private GameObject levelCompletedText;

    public bool IsPlaying { get; set; }

    public bool GameEnded { get; set; }

    private void Awake()
    {
        endings = FindObjectOfType<Endings>();
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

        endings.CheckGameFailedEnding();
        GameEnded = endings.GameEnded;
    }

    public void LevelCompleted()
    {
        AkSoundEngine.PostEvent("LevelCompleted_event", gameObject);
        levelCompletedText.SetActive(true);
        Time.timeScale = 0;
        IsPlaying = false; // Stops background rotation

        endings.CheckGameCompletedEnding();
        GameEnded = endings.GameEnded;
    }

    public void GoToLevelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }
}
