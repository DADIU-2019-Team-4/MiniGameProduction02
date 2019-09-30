using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SinisterFlashes : MonoBehaviour
{
    [SerializeField]
    private Image sinisterFlashingImage;

    public float Interval;
    [SerializeField]
    private float lengthOfFlash = 0.2f;

    [SerializeField]
    private float maxAlphaValue = 0.5f;

    [SerializeField]
    private bool startFlashing;
    private float timer;

    private void Start()
    {
        sinisterFlashingImage.DOFade(0, 0);
        sinisterFlashingImage.gameObject.SetActive(false);
    }

    public void StartDevilDealFlashing()
    {
        startFlashing = true;
    }

    private void Update()
    {
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
        sinisterFlashingImage.gameObject.SetActive(true);
        sinisterFlashingImage.DOFade(maxAlphaValue, lengthOfFlash / 2);
        yield return new WaitForSeconds(lengthOfFlash / 2);
        sinisterFlashingImage.DOFade(0, lengthOfFlash / 2);
        yield return new WaitForSeconds(lengthOfFlash / 2);
        sinisterFlashingImage.gameObject.SetActive(false);
    }
}
