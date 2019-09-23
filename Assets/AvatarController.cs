using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    public void Tick()
    {

    }

    public enum ThrownFrom
    {
        None,
        LeftHand,
        RightHand,
    }

    public enum ThrowMove
    {
        Horizontal,
        Cascade,
        Fountain,
    }

    public enum ThrownItem
    {
        Ball,
        Torch,
        Chainsaw,
        Knife,
        Bottles,
    }


    public void Throw(ThrownFrom hand, ThrowMove move, ThrownItem item)
    {
        
    }



}
