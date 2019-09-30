﻿using UnityEngine;

public abstract class DevilDeal : ScriptableObject
{
    [TextArea(10, 100)]
    public string dealDescription;

    public virtual void ApplyDevilDeal()
    {
        // override in child class
    }
}