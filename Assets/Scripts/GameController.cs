using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private InputController InputController;
    private ScoreController ScoreController;
    private SceneController sceneController;
    private ProgressionController progressionController;
    private Hand[] hands;

    public Transform leftHand;
    public Transform rightHand;
    public ParticleSystem rightHandParticle;
    public ParticleSystem leftHandParticle;

    public GameObject ball;
    public int numberOfBalls;
    public float distanceBetweenBalls;

    private void Awake()
    {
        InputController = FindObjectOfType<InputController>();
        ScoreController = FindObjectOfType<ScoreController>();
        sceneController = FindObjectOfType<SceneController>();
        progressionController = FindObjectOfType<ProgressionController>();
        hands = FindObjectsOfType<Hand>();
        Time.timeScale = 0.5f; 

        SpawnBalls(3);
    }

    // Update is called once per frame
    private void Update()
    {
        //OptionController.Tick();
        //AvatarController.Tick();
        //ScoreController.Tick();

        // Maybe we don't need these?
        //GraphicController.Tick();
        //AudioController.Tick();
    }

    public void Fail()
    {
        leftHandParticle.Play();
        rightHandParticle.Play();
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var ball in balls)
        {
            Destroy(ball);
        }
        foreach (var hand in hands)
        {
            hand.numberOfBalls = 0;
            hand.ball.Clear();
        }
        progressionController.UpdateProgression(ProgressionController.CatchType.FailedCatch);
        sceneController.IsPlaying = false;
        StartCoroutine(Delay());
    }

    void SpawnBalls(int number)
    {
        for (int i = 1; i <= number; i++)
        {
            if (i % 2 == 0)
            {
                Instantiate(ball, new Vector3(rightHand.transform.position.x + distanceBetweenBalls * (Mathf.Round(i / 2) - 1), rightHand.transform.position.y, rightHand.transform.position.z), rightHand.transform.rotation);
            }
            else
            {
                Instantiate(ball, new Vector3(leftHand.transform.position.x - distanceBetweenBalls * (Mathf.Round(i / 2) - 1), leftHand.transform.position.y, leftHand.transform.position.z), rightHand.transform.rotation);
            }
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        SpawnBalls(numberOfBalls);
    }
}


