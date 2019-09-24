using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private ScoreController ScoreController;

    // Insert prefab in editor.
    [SerializeField]
    private GameObject BallPrefab;

    // Use these to keep track of balls.
    private List<GameObject> Balls;

    void Awake()
    {
        ScoreController = FindObjectOfType<ScoreController>();
    }
}
