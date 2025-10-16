using System.Collections.Generic;

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PresetData", menuName = "ScriptableObjects/SPUM_PresetData", order = 1)]
[Serializable]
public class SPUM_PresetData : ScriptableObject
{
    //Assumed Stats for player to have ( Damn it bluebeard I DUNNO WHAT WE ARE DOING HERE )

    [Header("Progression")]
    public int StageLevel = 0;
    public int CollectedPoints = 0;

    [Header("Health Tracker")]
    public int MaxHealth = 5;
    public int CurrentHealth = 5;

    [Header("Ability Tracker")]
    public int MaxAbilityCharges = 0;
    public int CurrentAbilityCharges = 0;

    [Header("Movement")]
    public float JumpHeight = 5f;
    public float FallSpeed = 1f;
    public float HorizontalSpeed = 5f;

    [Header("Time Tracker")]
    public float TimeElapsed = 0f;

    //Upon fulfilling certain conditions from which Stats need to be reset this'll be the result.
    public void ResetStats()
    {
        CollectedPoints = 0;
        CurrentHealth = MaxHealth;
        CurrentAbilityCharges = MaxAbilityCharges;
        TimeElapsed = 0f;
    }
}