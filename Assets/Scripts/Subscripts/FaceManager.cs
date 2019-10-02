using Boo.Lang;
using UnityEngine;

public class FaceManager : MonoBehaviour
{
    [SerializeField]
    private GameObject face1;
    [SerializeField]
    private GameObject face2;
    [SerializeField]
    private GameObject face3;
    [SerializeField]
    private GameObject face4;
    [SerializeField]
    private GameObject face5;

    private List<GameObject> faces = new List<GameObject>();

    private void Start()
    {
        faces.Add(face1);
        faces.Add(face2);
        faces.Add(face3);
        faces.Add(face4);
        faces.Add(face5);
    }

    private void DeleteFaces()
    {
        foreach (GameObject face in faces)
        {
            face.SetActive(false);
        }
    }

    public void ChangeFace(int stageNumber)
    {
        DeleteFaces();

        if (stageNumber == 1)
            face1.SetActive(true);
        else if (stageNumber == 2)
            face2.SetActive(true);
        else if (stageNumber == 3)
            face3.SetActive(true);
        else if (stageNumber == 4)
            face4.SetActive(true);
        else
            face5.SetActive(true);
    }
}
