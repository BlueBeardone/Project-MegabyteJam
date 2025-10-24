using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]

public class PlayerStats : ScriptableObject
{
    // Attempting Initializing

    [SerializeField] PlayerStats Player_Stats;

    //Assumed Stats for Player ( Jackplsdon'tmessitupagain )

    [Header("Progression")]
    public int StageLevel = 0;
    public int CollectedPoints = 0;

    [Header("Health Tracker")]
    public int MaximumHealth = 5;
    public int CurrentHealth = 5;

    [Header("Movement")]
    public float FallSpeed = 1f;
    public float JumpHeight = 5f;
    public float Speed = 1f;
    public bool hasJumped = false;

    [Header("Time Tracker")]
    public float TimeElapsed = 0f;

    


    //Upon fulfilling certain conditions that would require a stat reset, this'll be the result

    public void ResetStats()
    {
        CollectedPoints = 0;
        TimeElapsed = 0f;
        CurrentHealth = MaximumHealth;
        hasJumped = false;
    }



}
