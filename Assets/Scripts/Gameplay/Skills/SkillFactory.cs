using System;
using UnityEngine;

public class SkillFactory 
{
    public ISkill CreateSkill(SkillType type)
    {
        switch (type)
        {
            case SkillType.DoubleJump:
                return new DoubleJumpSkill();
            case SkillType.Dash:
                return new DashSkill();
            case SkillType.GroundSlam:
                return new GroundSlamSKill();
            default:
                throw new ArgumentException($"Invalid skill type: {type}");
        }
    }
}
