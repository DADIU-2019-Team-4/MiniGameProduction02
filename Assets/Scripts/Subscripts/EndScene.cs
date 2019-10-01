using UnityEngine;

public class EndScene : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonsEnglish;
    [SerializeField]
    private GameObject buttonsDanish;
    [SerializeField]
    private GameObject backgroundEnglish;
    [SerializeField]
    private GameObject backgroundDanish;
    [SerializeField]
    private GameObject resetGameContainer;
    [SerializeField]
    private GameObject resetGameEnglish;
    [SerializeField]
    private GameObject resetGameDanish;


    private MenuController MenuController;

    private void Awake()
    {
        MenuController = FindObjectOfType<MenuController>();
    }

    private void Start()
    {
        if (MenuController.language == "English")
        {
            buttonsEnglish.SetActive(true);
            buttonsDanish.SetActive(false);
            backgroundEnglish.SetActive(true);
            backgroundDanish.SetActive(false);
            resetGameEnglish.SetActive(true);
            resetGameDanish.SetActive(false);
        }
        else if (MenuController.language == "Danish")
        {
            buttonsEnglish.SetActive(false);
            buttonsDanish.SetActive(true);
            backgroundEnglish.SetActive(false);
            backgroundDanish.SetActive(true);
            resetGameEnglish.SetActive(false);
            resetGameDanish.SetActive(true);
        }

        resetGameContainer.SetActive(false);
    }

    public void CheckDecision()
    {
        resetGameContainer.SetActive(true);
    }

    public void GoBack()
    {
        resetGameContainer.SetActive(false);
    }
}
