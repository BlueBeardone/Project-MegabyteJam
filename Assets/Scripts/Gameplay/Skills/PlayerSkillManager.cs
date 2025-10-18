using UnityEngine;
using System.Collections.Generic;

public class PlayerSkillManager : MonoBehaviour
{
    [SerializeField] private List<SkillType> unlockedSkills = new List<SkillType>();
    
    private Dictionary<SkillType, ISkill> skillInstances = new Dictionary<SkillType, ISkill>();
    private Dictionary<SkillType, float> skillCooldowns = new Dictionary<SkillType, float>();
    private SkillFactory skillFactory = new SkillFactory();

    private void Start()
    {
        InitializeSkills();
    }

    private void InitializeSkills()
    {
        foreach (SkillType type in unlockedSkills)
        {
            ISkill skill = skillFactory.CreateSkill(type);
            skillInstances[type] = skill;
            skillCooldowns[type] = 0f;
        }
    }

    private void Update()
    {
        UpdateCooldowns();
        HandleInput();
    }

    private void UpdateCooldowns()
    {
        List<SkillType> keys = new List<SkillType>(skillCooldowns.Keys);
        foreach (var skillType in keys)
        {
            if (skillCooldowns[skillType] > 0)
            {
                skillCooldowns[skillType] -= Time.deltaTime;
            }
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && HasSkill(SkillType.DoubleJump))
            TryExecuteSkill(SkillType.DoubleJump);
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && HasSkill(SkillType.Dash))
            TryExecuteSkill(SkillType.Dash);
        
        if (Input.GetKeyDown(KeyCode.Alpha3) && HasSkill(SkillType.GroundSlam))
            TryExecuteSkill(SkillType.GroundSlam);
    }

    public bool TryExecuteSkill(SkillType type)
    {
        if (!HasSkill(type) || skillCooldowns[type] > 0) return false;

        skillInstances[type].Execute(gameObject);
        skillCooldowns[type] = skillInstances[type].Cooldown;
        return true;
    }

    public void UnlockSkill(SkillType type)
    {
        if (!unlockedSkills.Contains(type))
        {
            unlockedSkills.Add(type);
            ISkill skill = skillFactory.CreateSkill(type);
            skillInstances[type] = skill;
            skillCooldowns[type] = 0f;
        }
    }

    public bool HasSkill(SkillType type) => unlockedSkills.Contains(type);
}
