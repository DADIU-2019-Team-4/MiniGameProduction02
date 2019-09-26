﻿using UnityEngine;

[CreateAssetMenu(fileName = "Devil Deal Time", menuName = "ScriptableObjects/DevilDealTime")]
public class DevilDealTime : DevilDeal
{
    public float timeToAdd;

    public override void ApplyDevilDeal()
    {
        ClockController clockController = FindObjectOfType<ClockController>();
        clockController.CurrentTimerValue += timeToAdd;

        clockController.UpdateArrow();
        clockController.UpdateClock();
    }
}