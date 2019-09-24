using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private InputController InputController;
    private OptionController OptionController;
    private AvatarController AvatarController;
    private ScoreManager ScoreController;
    private GraphicController GraphicController;
    private AudioController AudioController;
    private Hand[] hands;

    public Transform leftHand;
    public Transform rightHand;

    public GameObject ball;
    public int numberOfBalls;
    public float distanceBetweenBalls;

    private void Awake()
    {
        InputController = FindObjectOfType<InputController>();
        OptionController = GetComponent<OptionController>();
        AvatarController = GetComponent<AvatarController>();
        ScoreController = FindObjectOfType<ScoreManager>();
        GraphicController = GetComponent<GraphicController>();
        AudioController = GetComponent<AudioController>();
        hands = FindObjectsOfType<Hand>();
        Time.timeScale = 0.8f;

        SpawnBalls(3);
    }

    // Update is called once per frame
    private void Update()
    {
        InputController.Tick();
        //OptionController.Tick();
        //AvatarController.Tick();
        //ScoreController.Tick();

        // Maybe we don't need these?
        //GraphicController.Tick();
        //AudioController.Tick();
    }

    public void Fail()
    {
        ScoreController.ReduceProgress();
        ScoreController.numberofCatches = 0;
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var ball in balls)
        {
            Destroy(ball, 0.5f);
        }
        foreach (var hand in hands)
        {
            hand.numberOfBalls = 0;
        }
        SpawnBalls(numberOfBalls);
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
}


