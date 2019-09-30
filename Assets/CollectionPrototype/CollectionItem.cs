using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionItem : MonoBehaviour
{
    CollectionItemSpawner cisScript;
    public string placement;
    public ParticleSystem particles;
    public GameObject brokenMesh;

    // Start is called before the first frame update
    void Start()
    {
        cisScript = GameObject.Find("SceneController").GetComponent<CollectionItemSpawner>();
        StartCoroutine(DestroyAfterSeconds());
        Instantiate(particles, gameObject.transform.position, Quaternion.identity);
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
        cisScript.IncrementItemsCollected();
    }

    private void OnDestroy()
    {
        //Instantiate(brokenMesh, gameObject.transform.position, Quaternion.identity); //Instantiate the broken mesh when hit
        cisScript.currentActivePlates--;
    }

    IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(cisScript.timeUntilItemsDissappear);

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

        Instantiate(particles, gameObject.transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }
}
