using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private AvatarController AC;
    public Hand leftHand;
    public Hand rightHand;
    void Start()
    {
        AC = GetComponent<AvatarController>();
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
    }
}
