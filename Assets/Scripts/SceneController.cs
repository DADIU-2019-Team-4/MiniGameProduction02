using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject failText;

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private float timerValue = 60f;

    private void Update()
    {
        timerValue -= Time.deltaTime;

        string minutes = Mathf.Floor(timerValue / 60).ToString("0");
        string seconds = (timerValue % 60).ToString("00");

        timerText.text = $"{minutes}:{seconds}";
    }

    public void SceneReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelFailed()
    {
        failText.SetActive(true);
    }
}
