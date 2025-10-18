using System.Collections;
using UnityEngine;

public class DashSkill : ISkill
{
    public SkillType Type => SkillType.Dash;

    public float Cooldown => 2f;

    public string Description => "Allows the player to dash forward quickly.";

    public void Execute(GameObject player)
    {
        player.GetComponent<PlayerMovement>()?.StartCoroutine(PerformDash(player));
    }

    private IEnumerator PerformDash(GameObject player)
    {
        var movement = player.GetComponent<PlayerMovement>(); // Waiting for Jack's PlayerMovement script
        float originalSpeed = movement.MoveSpeed;
        movement.MoveSpeed *= 3f;
        yield return new WaitForSeconds(0.2f);
        movement.MoveSpeed = originalSpeed;
    }
}
