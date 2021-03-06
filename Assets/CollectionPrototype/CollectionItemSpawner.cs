﻿using System.Collections;
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

    public int currentActivePlates;
    public int maxActivePlates;
    public int ItemsCollected;
    public int NumberOfItemsToGoal;
    public float timeUntilItemsDissappear;

    private string mostRecentPosition;

    public Text CollectedText;
    public Text GoalText;

    private ScoreController ScoreController;
    private SceneController SceneController;
    private TutorialManager TutorialManager;
    private bool _firstAreaSpawned;
    private bool _tutorialLevel;
    private bool _fisrtCollectSound = false;
    private bool _secondCollectSound = false;
    private bool _thirdCollectSound = false;

    private void Awake()
    {
        SceneController = FindObjectOfType<SceneController>();

        if (FindObjectOfType<TutorialManager>() != null)
        {
            TutorialManager = FindObjectOfType<TutorialManager>();
            _firstAreaSpawned = false;
            _tutorialLevel = true;
        }
        else
            _tutorialLevel = false;
        ScoreController = FindObjectOfType<ScoreController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        if (_tutorialLevel)
            if (!_firstAreaSpawned)
                return;
        if (SceneController.GameEnded) return;

        if (isTop == false && currentActivePlates < maxActivePlates && mostRecentPosition != "top")
        {
            StartCoroutine(randomSpawn(Random.Range(minTime, maxTime), spawnPosTop, "top"));
            isTop = true;
        }

        if (isMid == false && currentActivePlates < maxActivePlates && mostRecentPosition != "mid")
        {
            StartCoroutine(randomSpawn(Random.Range(minTime, maxTime), spawnPosMid, "mid"));
            isMid = true;
        }

        if (isBottom == false && currentActivePlates < maxActivePlates && mostRecentPosition != "bottom")
        {
            StartCoroutine(randomSpawn(Random.Range(minTime, maxTime), spawnPosBottom, "bottom"));
            isBottom = true;
        }
    }

    private IEnumerator randomSpawn(float _seconds, GameObject _spawnPos, string _placement)
    {
        yield return new WaitForSeconds(_seconds);
        if (currentActivePlates < maxActivePlates && !SceneController.GameEnded)
        {
            GameObject item = Instantiate(prefab, _spawnPos.transform.position, Quaternion.Euler(90, 0, 130));
            AkSoundEngine.PostEvent("TargetSpawn_event", gameObject);
            item.transform.position = _spawnPos.transform.position;
            item.gameObject.GetComponent<CollectionItem>().placement = _placement;
            mostRecentPosition = _placement;
            currentActivePlates ++;
        }
    }

    public void IncrementItemsCollected(bool wasPerfectlyThrown)
    {
        ItemsCollected++;
        if ((ItemsCollected*100 / NumberOfItemsToGoal) > 25 && !_fisrtCollectSound)
        {
            AkSoundEngine.PostEvent("PlateCount1_event", gameObject);
            Debug.Log("FirstSound");
            _fisrtCollectSound = true;
        }
        if ((ItemsCollected*100 / NumberOfItemsToGoal) >50 && !_secondCollectSound)
        {
            AkSoundEngine.PostEvent("PlateCount2_event", gameObject);
            Debug.Log("SecondSound");
            _secondCollectSound = true;
        }
        if ((ItemsCollected*100 / NumberOfItemsToGoal)> 75 && !_thirdCollectSound)
        {
            AkSoundEngine.PostEvent("PlateCount3_event", gameObject);
            Debug.Log("ThirdSound");
            _thirdCollectSound = true;
        }
        UpdateText();
        ScoreController.IncrementScore(wasPerfectlyThrown);

        if (ItemsCollected == NumberOfItemsToGoal)
            SceneController.LevelCompleted();
    }

    public void UpdateText()
    {
        CollectedText.text = ItemsCollected.ToString();
        GoalText.text = NumberOfItemsToGoal.ToString();
    }

    public void DroppedItem()
    {
        ScoreController.ResetMultiplier();
    }

    public void SpawnTutorialObject()
    {
        if (!_firstAreaSpawned)
        {
            StartCoroutine(randomSpawn(0f, spawnPosTop, "top"));
            isTop = true;
            _firstAreaSpawned = true;
        }
    }
}
