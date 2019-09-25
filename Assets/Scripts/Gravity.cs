using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravity;
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, gravity, 0);
    }
}