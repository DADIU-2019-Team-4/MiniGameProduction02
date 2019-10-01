using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionItem : MonoBehaviour
{
    CollectionItemSpawner CollectionItemSpawner;
    public string placement;
    public ParticleSystem particles;
    public GameObject brokenMesh;

    // Start is called before the first frame update
    void Start()
    {
        CollectionItemSpawner = GameObject.Find("SceneController").GetComponent<CollectionItemSpawner>();
        StartCoroutine(DestroyAfterSeconds());
        Instantiate(particles, gameObject.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            if (placement == "top")
            {
                CollectionItemSpawner.isTop = false;
            }
            else if (placement == "mid")
            {
                CollectionItemSpawner.isMid = false;
            }
            else if (placement == "bottom")
            {
                CollectionItemSpawner.isBottom = false;
            }
            AkSoundEngine.PostEvent("TargetCollect_event", gameObject);
            Destroy(this.gameObject);
            if (FindObjectOfType<TutorialManager>() != null)
                FindObjectOfType<TutorialManager>().EnableTutorialUI();
            CollectionItemSpawner.IncrementItemsCollected(other.GetComponent<Ball>().wasPerfectlyThrown);
            GameObject go = Instantiate(brokenMesh, gameObject.transform.position, Quaternion.Euler(90, 0, 130));
            go.GetComponent<Rigidbody>().AddExplosionForce(10f, go.transform.position, 5f);

        }
    }

    private void OnDestroy()
    {
        //Instantiate(brokenMesh, gameObject.transform.position, Quaternion.identity); //Instantiate the broken mesh when hit
        CollectionItemSpawner.currentActivePlates--;
    }

    IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(CollectionItemSpawner.timeUntilItemsDissappear);

        if (placement == "top")
        {
            CollectionItemSpawner.isTop = false;
        }
        else if (placement == "mid")
        {
            CollectionItemSpawner.isMid = false;
        }
        else if (placement == "bottom")
        {
            CollectionItemSpawner.isBottom = false;
        }
        AkSoundEngine.PostEvent("TargetDestroy_event", gameObject);
        Instantiate(particles, gameObject.transform.position, Quaternion.identity);
        CollectionItemSpawner.DroppedItem();
        Destroy(this.gameObject);
    }
}
