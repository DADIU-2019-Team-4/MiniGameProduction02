using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    SceneController SceneController;

    [SerializeField]
    private GameObject life;

    [SerializeField]
    private GameObject lifeSpawnPoint;

    [SerializeField]
    private int maxLives = 3;

    public int CurrentLives { get; set; }

    [SerializeField]
    private float offset = 15;

    private float imageWidth;

    private void Awake()
    {
        SceneController = FindObjectOfType<SceneController>();
    }

    void Start()
    {
        imageWidth = life.GetComponent<RectTransform>().rect.width;
        CurrentLives = maxLives;
        SpawnLives();
    }

    private void DeleteLives()
    {
        Image[] allLives = lifeSpawnPoint.GetComponentsInChildren<Image>();
        foreach (Image allLife in allLives)
            Destroy(allLife.gameObject);
    }

    private void SpawnLives()
    {
        for (int i = 0; i < CurrentLives; i++)
        {
            GameObject lifeObj = Instantiate(life, lifeSpawnPoint.transform);
            Vector3 iconPos = lifeObj.transform.localPosition;
            float newXPos = iconPos.x + (imageWidth + offset) * i;
            lifeObj.transform.localPosition = new Vector3(newXPos, iconPos.y);
        }
    }

    public void UpdateLives()
    {
        // first delete the sprites
        DeleteLives();

        if (CurrentLives <= 0)
        {
            SceneController.LevelFailed();
            return;
        }

        // then draw new sprites
        SpawnLives();
    }
}
