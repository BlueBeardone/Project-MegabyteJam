using UnityEngine;

public class GroundSlamSkill : ISkill
{
    public SkillType Type => SkillType.GroundSlam;
    public float Cooldown => 1.5f;
    public string Description => "Slam down to the ground from air";

    public void Execute(GameObject player)
    {
        var body = player.GetComponent<PlayerSkillManager>().playerRigidbody;
        body.linearVelocity = new Vector2(body.linearVelocity.x, -20f);
        Debug.Log("Ground slam executed!");
    }
}