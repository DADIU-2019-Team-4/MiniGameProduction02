using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Hand leftHand;
    public Hand rightHand;
    public GameObject ball1;
    void Start()
    {
   
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        PcInput();
#elif UNITY_ANDROID || UNITY_IOS
        MobileInput();
#endif
    }


    private void PcInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            leftHand.Throw("Left","Up");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            leftHand.Throw("Left","Down");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            leftHand.Throw("Left","Right");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rightHand.Throw("Right","Up");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rightHand.Throw("Right","Down");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            rightHand.Throw("Right","Left");
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("SpaceBar pressed");
            LevelReset();
        }
    }
    public void LevelReset()
    {
        Debug.Log("Level reset");
        var ball = GameObject.FindGameObjectWithTag("Ball");
        Destroy(ball);
        Instantiate(ball1);
    }
}
