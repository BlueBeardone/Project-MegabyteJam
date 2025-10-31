using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Objects/SkillData")]
public class SkillData : ScriptableObject
{
    [Header("Unlocked Skills")]
    public List<SkillType> unlockedSkills = new List<SkillType>();
    
    [Header("Skill Cooldowns")]
    public float dashCooldown = 2f;
    public float groundSlamCooldown = 1.5f;
    
    [Header("Skill Settings")]
    public float dashSpeedMultiplier = 3f;
    public float dashDuration = 0.3f;
    
    public void ResetSkills()
    {
        unlockedSkills.Clear();
    }
    
    public void UnlockSkill(SkillType skill)
    {
        if (!unlockedSkills.Contains(skill))
        {
            unlockedSkills.Add(skill);
        }
    }
    
    public bool HasSkill(SkillType skill)
    {
        return unlockedSkills.Contains(skill);
    }
}