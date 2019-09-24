using UnityEngine;

public class Wall : MonoBehaviour
{
    private ProgressionController progressionController;
    private BallController ballController;

    private void Awake()
    {
        progressionController = FindObjectOfType<ProgressionController>();
        ballController = FindObjectOfType<BallController>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            progressionController.UpdateProgression(ProgressionController.CatchType.FailedCatch);
            ballController.RemoveBall(col.gameObject);
            ballController.SpawnBall();
        }
    }
}
