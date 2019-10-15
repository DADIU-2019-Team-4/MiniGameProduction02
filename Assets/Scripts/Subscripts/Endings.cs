using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Endings : MonoBehaviour
{
    private DevilDealController DevilDealController;

    [SerializeField]
    private Image fadeImage;

    [SerializeField]
    private float fadeDuration = 7;

    [SerializeField]
    private float dialogueDuration = 1;

    public bool GameEnded { get; set; }

    private void Awake()
    {
        DevilDealController = FindObjectOfType<DevilDealController>();
        fadeImage.gameObject.SetActive(false);
        fadeImage.DOFade(0, 0);
    }

    public void CheckGameCompletedEnding()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        int levelNumber = int.Parse(sceneName.Substring(sceneName.Length - 1));

        if (levelNumber != 0) return;

        if (DevilDealController.AcceptedNegativeDealsCount == 0)
            StartCoroutine(HappyEnding());
        else if (DevilDealController.AcceptedNegativeDealsCount >= 1 &&
                 DevilDealController.AcceptedNegativeDealsCount <= 5)
            StartCoroutine(NeutralEnding());
        else if (DevilDealController.AcceptedNegativeDealsCount > 5 &&
                 DevilDealController.AcceptedNegativeDealsCount <= DevilDealController.MaxDevilDeals)
            StartCoroutine(BadEnding());

        GameEnded = true;
        DestroyRemainingObjects();

        Debug.Log("Triggered ending at Level " + levelNumber);
    }

    public void CheckGameFailedEnding()
    {
        if (DevilDealController.AcceptedNegativeDealsCount == DevilDealController.MaxDevilDeals)
        {
            StartCoroutine(TrueBadEnding());
            GameEnded = true;
            DestroyRemainingObjects();
        }
    }

    /// <summary>
    /// This ending triggers when Viola completes the game with no devil deals.
    /// </summary>
    private IEnumerator HappyEnding()
    {
        Debug.Log("Triggered Happy Ending");

        Time.timeScale = 1;

        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(1, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);

        // todo insert voice line happy ending (for Viola)

        yield return new WaitForSeconds(dialogueDuration);
        SceneManager.LoadScene("EndScene");
    }

    /// <summary>
    /// This ending triggers when Viola completes the game with 1-5 devil deals.
    /// </summary>
    private IEnumerator NeutralEnding()
    {
        Debug.Log("Triggered Neutral Ending");

        Time.timeScale = 1;

        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(1, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);

        // todo insert voice line neutral ending (for Viola)

        yield return new WaitForSeconds(dialogueDuration);
        SceneManager.LoadScene("EndScene");
    }

    /// <summary>
    /// This ending triggers when Viola completes the game with 6-9 devil deals.
    /// </summary>
    private IEnumerator BadEnding()
    {
        Debug.Log("Triggered Bad Ending");

        Time.timeScale = 1;

        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(1, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);

        // todo insert voice line bad ending (for Viola)

        yield return new WaitForSeconds(dialogueDuration);
        SceneManager.LoadScene("EndScene");
    }

    /// <summary>
    /// This ending triggers when Viola fails the game and accepted all devil deals.
    /// </summary>
    private IEnumerator TrueBadEnding()
    {
        Debug.Log("Triggered True Bad Ending");

        Time.timeScale = 1;

        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(1, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);

        // todo insert voice line true bad ending (for Viola)

        yield return new WaitForSeconds(dialogueDuration);
        SceneManager.LoadScene("EndScene");
    }

    private void DestroyRemainingObjects()
    {
        Ball[] balls = FindObjectsOfType<Ball>();
        foreach (Ball ball in balls)
            Destroy(ball.gameObject);

        CollectionItem[] items = FindObjectsOfType<CollectionItem>();
        foreach (CollectionItem item in items)
            Destroy(item.gameObject);
    }
}
