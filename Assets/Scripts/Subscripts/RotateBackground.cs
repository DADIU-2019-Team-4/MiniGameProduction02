using UnityEngine;

public class RotateBackground : MonoBehaviour
{
    private SceneController sceneController;

    [SerializeField]
    private GameObject Background;
    private GameObject Lighting;

    public float RotationSpeed = 7;

    private float value;

    private void Awake()
    {
        sceneController = FindObjectOfType<SceneController>();
        value = 0;
        Lighting = GameObject.Find("Lighting");
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
            Vector3 rotation = new Vector3(0, -180 + value * RotationSpeed, 0);
            Background.GetComponent<Transform>().eulerAngles = rotation;
            Lighting.GetComponent<Transform>().eulerAngles = rotation;
        }
    }
}
