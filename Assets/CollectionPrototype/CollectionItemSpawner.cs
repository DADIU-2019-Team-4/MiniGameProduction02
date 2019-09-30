using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionItemSpawner : MonoBehaviour
{
    public float minTime = 2f;
    public float maxTime = 8f;

    public GameObject spawnPosTop;
    public GameObject spawnPosMid;
    public GameObject spawnPosBottom;

    public GameObject prefab;

    public bool isTop = false;
    public bool isMid = false;
    public bool isBottom = false;

    public int ItemsCollected;
    public int NumberOfItemsToGoal = 20;

    public Text CollectableText;

    private ScoreController ScoreController;
    private SceneController SceneController;

    private void Awake()
    {
        SceneController = FindObjectOfType<SceneController>();
        ScoreController = FindObjectOfType<ScoreController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CollectableText.text = $"{ItemsCollected}\\{NumberOfItemsToGoal}";
    }

    // Update is called once per frame
    void Update()
    {
        if (isTop == false)
        {
            StartCoroutine(randomSpawn(Random.Range(minTime, maxTime), spawnPosTop, "top"));
            isTop = true;
        }

        if (isMid == false)
        {
            StartCoroutine(randomSpawn(Random.Range(minTime, maxTime), spawnPosMid, "mid"));
            isMid = true;
        }

        if (isBottom == false)
        {
            StartCoroutine(randomSpawn(Random.Range(minTime, maxTime), spawnPosBottom, "bottom"));
            isBottom = true;
        }
    }

    private IEnumerator randomSpawn(float _seconds, GameObject _spawnPos, string _placement)
    {
        yield return new WaitForSeconds(_seconds);


        GameObject item = Instantiate(prefab, _spawnPos.transform.position, Quaternion.identity);
        item.transform.position = _spawnPos.transform.position;
        item.gameObject.GetComponent<CollectionItem>().placement = _placement;
        print("spawned top");
    }

    public void IncrementItemsCollected(bool wasPerfectlyThrown)
    {
        ItemsCollected++;
        CollectableText.text = $"{ItemsCollected}\\{NumberOfItemsToGoal}";
        ScoreController.IncrementScore(wasPerfectlyThrown);

        if (ItemsCollected == NumberOfItemsToGoal)
            SceneController.LevelCompleted();
    }
}
