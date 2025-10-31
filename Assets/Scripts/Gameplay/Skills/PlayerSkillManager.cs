using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSkillManager : MonoBehaviour
{
    [Header("Skill Data")]
    public SkillData skillData;
    
    [Header("Skill References")]
    //public GameObject fireballPrefab;
    
    // Runtime cooldown tracking
    private Dictionary<SkillType, float> currentCooldowns = new Dictionary<SkillType, float>();
    private SkillFactory skillFactory = new SkillFactory();
    private Dictionary<SkillType, ISkill> skillInstances = new Dictionary<SkillType, ISkill>();
    
    // Components
    private PlayerStats stats;
    private Rigidbody2D body;
    private PlayerMovement movement;
    
    // Double jump tracking
    private bool hasDoubleJumped = false;
    
    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        
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
        }
        
        // Ground Slam - S or Down Arrow
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && 
            HasSkill(SkillType.GroundSlam))
        {
            TryExecuteSkill(SkillType.GroundSlam);
        }
    }
    
    private void HandleDoubleJump()
    {
        // Double jump is handled separately since it uses the same input as regular jump
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && 
            stats.hasJumped && HasSkill(SkillType.DoubleJump) && !hasDoubleJumped)
        {
            TryExecuteSkill(SkillType.DoubleJump);
            hasDoubleJumped = true;
        }
        
        // Reset double jump when landing
        if (!stats.hasJumped)
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
            //SkillType.Fireball => skillData.fireballCooldown,
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
    
    // Public methods for skills to access
    public PlayerStats GetPlayerStats() => stats;
    public Rigidbody2D GetRigidbody() => body;
    public SkillData GetSkillData() => skillData;
}