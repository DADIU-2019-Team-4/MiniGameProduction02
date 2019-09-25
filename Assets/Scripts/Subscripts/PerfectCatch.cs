using System.Collections.Generic;
using UnityEngine;

public class PerfectCatch : MonoBehaviour
{
    //public bool perfectCatch;
    public List<GameObject> In = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        In.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        In.Remove(other.gameObject);
    }
}
