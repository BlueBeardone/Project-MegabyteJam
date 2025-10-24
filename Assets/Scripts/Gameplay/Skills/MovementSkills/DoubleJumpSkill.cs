using System;
using UnityEngine;

public class DoubleJumpSkill : ISkill
{
    public SkillType Type => SkillType.DoubleJump;

    public Boolean secondJump = true;

    public float Cooldown => 2.0f;

    public string Description => "Allows the player to jump a second time while in the air.";

    public void Execute(GameObject player)
    {
        var movement = player.GetComponent<PlayerMovement>(); // Waiting for Jack's PlayerMovement script
        if (movement != null && !movement.stats.hasJumped && secondJump)
        {
            movement.Jumping();
            secondJump = false;
        }
    }
}
