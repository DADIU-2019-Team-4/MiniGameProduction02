using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject levelFailedText;

    [SerializeField]
    private GameObject levelCompletedText;

    public bool IsPlaying { get; set; }

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
