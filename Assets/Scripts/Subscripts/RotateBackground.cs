using UnityEngine;

public class RotateBackground : MonoBehaviour
{
    private SceneController sceneController;

    [SerializeField]
    private GameObject Background;

    public float RotationSpeed = 7;

    private float value;

    private void Awake()
    {
        sceneController = FindObjectOfType<SceneController>();
        value = 0;
    }

    private void Update()
    {
        if (sceneController.IsPlaying)
            RotateScenery();
    }

    private void RotateScenery()
    {
        if (Background != null)
        {
            value += Time.deltaTime * (1 / Time.timeScale);
            Background.GetComponent<Transform>().eulerAngles = new Vector3(0, value * RotationSpeed, 0);
        }
    }
}
