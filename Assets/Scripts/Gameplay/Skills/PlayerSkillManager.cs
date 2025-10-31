using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerSkillManager : MonoBehaviour
{
    [Header("Skill Data")]
    public SkillData skillData;
    
    [Header("Player References")]
    public PlayerStats playerStats; // Assign in inspector
    public Rigidbody2D playerRigidbody; // Assign in inspector
    
    // Runtime cooldown tracking
    private Dictionary<SkillType, float> currentCooldowns = new Dictionary<SkillType, float>();
    private SkillFactory skillFactory = new SkillFactory();
    private Dictionary<SkillType, ISkill> skillInstances = new Dictionary<SkillType, ISkill>();
    
    // Double jump tracking
    private bool hasDoubleJumped = false;
    
    private void Awake()
    {
        InitializeSkillSystem();
    }
    
    private void InitializeSkillSystem()
    {
        // Create skill instances for all unlocked skills
        foreach (SkillType type in skillData.unlockedSkills)
        {
            ISkill skill = skillFactory.CreateSkill(type);
            skillInstances[type] = skill;
            currentCooldowns[type] = 0f;
        }
    }
    
    private void Update()
    {
        UpdateCooldowns();
        HandleSkillInput();
        HandleDoubleJump();
    }
    
    private void UpdateCooldowns()
    {
        List<SkillType> keys = new List<SkillType>(currentCooldowns.Keys);
        foreach (var skillType in keys)
        {
            if (currentCooldowns[skillType] > 0)
            {
                currentCooldowns[skillType] -= Time.deltaTime;
            }
        }
    }
    
    private void HandleSkillInput()
    {
        // Dash - Left Shift
        if (Input.GetKeyDown(KeyCode.LeftShift) && HasSkill(SkillType.Dash))
        {
            TryExecuteSkill(SkillType.Dash);
            Debug.Log("Dash executed");
            Console.WriteLine("Dash executed");
        }
        
        // Ground Slam - S or Down Arrow
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && 
            HasSkill(SkillType.GroundSlam))
        {
            TryExecuteSkill(SkillType.GroundSlam);
            Debug.Log("Ground Slam executed");
            Console.WriteLine("Ground Slam executed");
        }
    }
    
    private void HandleDoubleJump()
    {
        // Double jump is handled separately since it uses the same input as regular jump
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && 
            playerStats.hasJumped && HasSkill(SkillType.DoubleJump) && !hasDoubleJumped)
        {
            TryExecuteSkill(SkillType.DoubleJump);
            hasDoubleJumped = true;
        }
        
        // Reset double jump when landing
        if (!playerStats.hasJumped)
        {
            hasDoubleJumped = false;
        }
    }
    
    public bool TryExecuteSkill(SkillType type)
    {
        if (!HasSkill(type) || currentCooldowns[type] > 0) 
            return false;

        skillInstances[type].Execute(gameObject);
        currentCooldowns[type] = GetCooldownTime(type);
        return true;
    }
    
    private float GetCooldownTime(SkillType type)
    {
        return type switch
        {
            SkillType.Dash => skillData.dashCooldown,
            SkillType.GroundSlam => skillData.groundSlamCooldown,
            _ => 0f
        };
    }
    
    public void UnlockSkill(SkillType type)
    {
        skillData.UnlockSkill(type);
        
        // Initialize the newly unlocked skill
        ISkill skill = skillFactory.CreateSkill(type);
        skillInstances[type] = skill;
        currentCooldowns[type] = 0f;
        
        Debug.Log($"Unlocked skill: {type}");
    }
    
    public bool HasSkill(SkillType type) => skillData.HasSkill(type);
    public float GetCurrentCooldown(SkillType type) => currentCooldowns.ContainsKey(type) ? currentCooldowns[type] : 0f;
}