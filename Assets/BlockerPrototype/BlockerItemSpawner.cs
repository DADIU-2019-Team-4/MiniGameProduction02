using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerItemSpawner : MonoBehaviour
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

    public int maxAmountBlockers = 2;
    public int currentBlockers = 0;
    public float destroyDelay = 3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isTop == false && currentBlockers < maxAmountBlockers)
        {
            StartCoroutine(randomSpawn(Random.Range(minTime, maxTime), spawnPosTop, "top"));
            isTop = true;
        }

        if (isMid == false && currentBlockers < maxAmountBlockers)
        {
            StartCoroutine(randomSpawn(Random.Range(minTime, maxTime), spawnPosMid, "mid"));
            isMid = true;
        }

        if (isBottom == false && currentBlockers < maxAmountBlockers)
        {
            StartCoroutine(randomSpawn(Random.Range(minTime, maxTime), spawnPosBottom, "bottom"));
            isBottom = true;
        }
    }

    private IEnumerator randomSpawn(float _seconds, GameObject _spawnPos, string _placement)
    {
        yield return new WaitForSeconds(_seconds);

        if (currentBlockers < maxAmountBlockers)
        {
            GameObject item = Instantiate(prefab, _spawnPos.transform.position, Quaternion.identity);
            item.transform.position = _spawnPos.transform.position;
            item.GetComponent<BlockerItem>().destroyDelay = destroyDelay;
            item.gameObject.GetComponent<BlockerItem>().placement = _placement;
            currentBlockers++;
        }

    }
}
