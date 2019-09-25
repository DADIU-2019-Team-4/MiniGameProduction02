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
    private List<GameObject> Balls = new List<GameObject>();

    void Awake()
    {
        ScoreController = FindObjectOfType<ScoreController>();
    }

    public void SpawnBall()
    {
        GameObject ball = Instantiate(BallPrefab);
        Balls.Add(ball);
    }

    public void RemoveBall(GameObject ball)
    {
        Balls.Remove(ball);
        Destroy(ball);
    }
}
