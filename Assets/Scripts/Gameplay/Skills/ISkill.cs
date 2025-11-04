using UnityEngine;

public interface ISkill
{
    SkillType Type { get; }
    void Execute(GameObject player);
    float Cooldown { get; }
    string Description { get; }
}