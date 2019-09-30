using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionItem : MonoBehaviour
{
    CollectionItemSpawner cisScript;
    public string placement;

    // Start is called before the first frame update
    void Start()
    {
        cisScript = GameObject.Find("SceneController").GetComponent<CollectionItemSpawner>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (placement == "top")
        {
            cisScript.isTop = false;
        }
        else if (placement == "mid")
        {
            cisScript.isMid = false;
        }
        else if (placement == "bottom")
        {
            cisScript.isBottom = false;
        }

        Destroy(this.gameObject);
        cisScript.IncrementItemsCollected(other.GetComponent<Ball>().wasPerfectlyThrown);

    }
}
