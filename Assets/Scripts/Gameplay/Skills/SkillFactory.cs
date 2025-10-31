using System;

public class SkillFactory
{
    public ISkill CreateSkill(SkillType type)
    {
        return type switch
        {
            SkillType.DoubleJump => new DoubleJumpSkill(),
            SkillType.Dash => new DashSkill(),
            SkillType.GroundSlam => new GroundSlamSkill(),
            _ => throw new ArgumentException($"Invalid skill type: {type}")
        };
    }
}