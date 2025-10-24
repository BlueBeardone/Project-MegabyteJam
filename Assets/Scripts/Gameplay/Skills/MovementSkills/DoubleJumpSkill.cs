using UnityEngine;

public class DoubleJumpSkill : ISkill
{
    public SkillType Type => SkillType.DoubleJump;
    public float Cooldown => 0f;
    public string Description => "Perform a second jump in mid-air";

    public void Execute(GameObject player)
    {
        var skillManager = player.GetComponent<PlayerSkillManager>();
        var stats = skillManager.GetStats();
        var body = skillManager.GetRigidbody();
        
        // Reset y velocity and jump again
        body.linearVelocity = new Vector2(body.linearVelocity.x, 0);
        body.AddForce(Vector2.up * stats.JumpHeight, ForceMode2D.Impulse);
        
        Debug.Log("Double jump executed!");
    }
}