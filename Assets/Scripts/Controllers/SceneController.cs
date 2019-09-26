using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject levelFailedText;

    [SerializeField]
    private GameObject levelCompletedText;

    public bool IsPlaying { get; set; }

    public void SceneReset()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelFailed()
    {
        levelFailedText.SetActive(true);
        Time.timeScale = 0;
    }

    public void LevelCompleted()
    {
        levelCompletedText.SetActive(true);
        Time.timeScale = 0;
    }

    public void GoToLevelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }
}
