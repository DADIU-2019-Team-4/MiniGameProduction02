using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/JuggleObject", order = 1)]
public class ScriptableJuggleObject : ScriptableObject
{
    public string objectName;
    public Mesh objectMesh;
    public float mass;
    public bool willStickToFloor;

}
