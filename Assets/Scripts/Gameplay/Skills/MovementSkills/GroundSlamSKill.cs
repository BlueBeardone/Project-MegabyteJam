using UnityEngine;

public class GroundSlamSKill : ISkill
{
    public SkillType Type => SkillType.GroundSlam;
    public float Cooldown => 1.5f;
    public string Description => "Slam down to the ground from air";

    public void Execute(GameObject player)
    {
        var rb = player.GetComponent<Rigidbody2D>();
        if (rb != null && !player.GetComponent<PlayerMovement>().IsGrounded) // Waiting for Jack's PlayerMovement script
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -20f);
        }
    }
}
