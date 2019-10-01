using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SinisterFlashes : MonoBehaviour
{
    private SceneController SceneController;

    public Image SinisterFlashingImage;

    public float Interval;
    [SerializeField]
    private float lengthOfFlash = 0.2f;

    [SerializeField]
    private float maxAlphaValue = 0.5f;

    [SerializeField]
    private bool startFlashing;
    private float timer;

    private void Awake()
    {
        SceneController = FindObjectOfType<SceneController>();
    }

    private void Start()
    {
        SinisterFlashingImage.DOFade(0, 0);
        SinisterFlashingImage.gameObject.SetActive(false);
    }

    public void StartDevilDealFlashing()
    {
        startFlashing = true;
    }

    private void Update()
    {
        if (SceneController.GameEnded)
            return;

        if (Time.timeScale != 0)
            timer += Time.deltaTime * (1 / Time.timeScale);

        if (startFlashing && timer >= Interval)
        {
            timer = 0;
            StartCoroutine(Flash());
        }
    }

    private IEnumerator Flash()
    {
        SinisterFlashingImage.gameObject.SetActive(true);
        SinisterFlashingImage.DOFade(maxAlphaValue, lengthOfFlash / 2);
        yield return new WaitForSeconds(lengthOfFlash / 2);
        SinisterFlashingImage.DOFade(0, lengthOfFlash / 2);
        yield return new WaitForSeconds(lengthOfFlash / 2);
        SinisterFlashingImage.gameObject.SetActive(false);
    }
}
