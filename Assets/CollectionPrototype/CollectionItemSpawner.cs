﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private SceneController SceneController;

    private void Awake()
    {
        SceneController = FindObjectOfType<SceneController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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

        if (currentActivePlates < maxActivePlates)
        {
            GameObject item = Instantiate(prefab, _spawnPos.transform.position, Quaternion.identity);
            item.transform.position = _spawnPos.transform.position;
            item.gameObject.GetComponent<CollectionItem>().placement = _placement;
            mostRecentPosition = _placement;
            currentActivePlates ++;
        }
    }

    public void IncrementItemsCollected()
    {
        ItemsCollected++;
        if (ItemsCollected == NumberOfItemsToGoal)
            SceneController.LevelCompleted();
    }
}
