using UnityEngine;
using UnityEngine.UI;

public class ProgressionController : MonoBehaviour
{
    [SerializeField]
    private Image filling;

    [SerializeField]
    private float startAmount = 0.5f;
    [SerializeField]
    private float depleteSpeed = 0.01f;

    [SerializeField]
    private float normalCatchAmount = 0.1f;
    [SerializeField]
    private float perfectCatchAmount = 0.2f;
    [SerializeField]
    private float failAmount = -0.1f;

    private SceneController sceneController;

    public enum CatchType { normalCatch, perfectCatch, FailedCatch }

    private void Awake()
    {
        sceneController = FindObjectOfType<SceneController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        filling.fillAmount = startAmount;
    }

    private void Update()
    {
        filling.fillAmount -= Time.deltaTime * depleteSpeed;

        if (filling.fillAmount <= 0)
            sceneController.LevelFailed();
    }

    public void UpdateProgression(CatchType catchType)
    {
        switch (catchType)
        {
            case CatchType.normalCatch:
                filling.fillAmount += normalCatchAmount;
                break;
            case CatchType.perfectCatch:
                filling.fillAmount += perfectCatchAmount;
                break;
            case CatchType.FailedCatch:
                filling.fillAmount += failAmount;
                break;
            default:
                break;
        }
    }
}
