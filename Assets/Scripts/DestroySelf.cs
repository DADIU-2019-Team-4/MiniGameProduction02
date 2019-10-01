using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{

    public float timeUntilDestroy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfter(timeUntilDestroy));
    }

    IEnumerator DestroyAfter(float _seconds)
    {
        yield return new WaitForSeconds(_seconds);
        Destroy(gameObject);
    }
}
