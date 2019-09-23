using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private GameObject ScoreControllerRef;
    private ScoreController ScoreController;

    // Insert prefab in editor.
    [SerializeField]
    private GameObject BallPrefab;

    // Use these to keep track of balls.
    private List<GameObject> Balls;

    // Start is called before the first frame update
    void Start()
    {
        ScoreController = ScoreControllerRef.GetComponent<ScoreController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
