using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerItem : MonoBehaviour
{
    BlockerItemSpawner cisScript;
    public string placement;

    public float destroyDelay = 0;

    bool hasRun = false;

    Color32 alpha;

    bool lerpAlpha = false;

    // Start is called before the first frame update
    void Start()
    {
        cisScript = GameObject.Find("SceneController").GetComponent<BlockerItemSpawner>();
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

        //alpha = gameObject.GetComponent<MeshRenderer>().material.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, destroyDelay);

        if (lerpAlpha)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = new Color(1f, 0.5f, 1f, Mathf.PingPong(Time.time, 1f));
            print("Lerps aplhga: "+ gameObject.GetComponent<MeshRenderer>().material.color.a);
        }

        StartCoroutine(Wait());
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject.Find("BallController").GetComponent<BallController>().BallDropped();
    }

    IEnumerator Wait()
    {
        lerpAlpha = true;
        gameObject.GetComponent<Collider>().enabled = false;
        //gameObject.GetComponent<MeshRenderer>().material.color = new Color(0f, 0.5f, 1f, Mathf.PingPong(Time.time, 1));
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Collider>().enabled = true;
        lerpAlpha = false;
    }

    private void OnDestroy() {
        cisScript.currentBlockers--;
    }
}
