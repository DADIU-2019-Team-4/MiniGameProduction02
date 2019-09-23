using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectCatch : MonoBehaviour
{
    public bool perfectCatch;
    private void Start()
    {
        perfectCatch = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        perfectCatch = true;
    }

    private void OnTriggerExit(Collider other)
    {
        perfectCatch = false;
    }
}
