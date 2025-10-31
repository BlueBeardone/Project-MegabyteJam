using UnityEngine;
using System.Collections;

public class DashSkill : ISkill
{
    public SkillType Type => SkillType.Dash;
    public float Cooldown => 2f;
    public string Description => "Quickly dash forward";

    public void Execute(GameObject player)
    {
        player.GetComponent<PlayerSkillManager>().StartCoroutine(PerformDash(player));
    }

    private IEnumerator PerformDash(GameObject player)
    {
        var skillManager = player.GetComponent<PlayerSkillManager>();
        var stats = skillManager.playerStats;
        var body = skillManager.playerRigidbody;
        var skillData = skillManager.skillData;
        
        float originalSpeed = stats.Speed;
        stats.Speed *= skillData.dashSpeedMultiplier;
        
        // Apply dash force
        float direction = Mathf.Sign(body.linearVelocity.x);
        if (direction == 0) direction = 1; // Default to right if standing still
        
        body.linearVelocity = new Vector2(direction * stats.Speed, body.linearVelocity.y);
        
        yield return new WaitForSeconds(skillData.dashDuration);
        
        // Restore original speed
        stats.Speed = originalSpeed;
    }
}