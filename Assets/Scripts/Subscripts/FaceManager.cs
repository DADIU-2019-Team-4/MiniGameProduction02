using UnityEngine;

public class FaceManager : MonoBehaviour
{
    [SerializeField]
    private GameObject head;

    [SerializeField]
    private Mesh face1;
    [SerializeField]
    private Mesh face2;
    [SerializeField]
    private Mesh face3;
    [SerializeField]
    private Mesh face4;
    [SerializeField]
    private Mesh face5;

    public void ChangeFace(int stageNumber)
    {
        MeshFilter face = head.GetComponent<MeshFilter>();
        if (stageNumber == 1)
            face.mesh = face1;
        else if (stageNumber == 2)
            face.mesh = face2;
        else if (stageNumber == 3)
            face.mesh = face3;
        else if (stageNumber == 4)
            face.mesh = face4;
        else
            face.mesh = face5;
    }
}
