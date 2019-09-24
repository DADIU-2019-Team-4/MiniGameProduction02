using UnityEngine;
using UnityEngine.UI;

public class ProgressionController : MonoBehaviour
{
    [SerializeField]
    private Image filling;

    [SerializeField]
    private float startAmount = 0.5f;

    [SerializeField]
    private float depleteSpeed = 0.1f;

    private SceneController sceneController;

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

    public void UpdateProgression()
    {

    }
}
