using UnityEngine;
using System.Collections.Generic;

public class PlayerSkillManager : MonoBehaviour
{
    [Header("Skill System")]
    [SerializeField] private List<SkillType> unlockedSkills = new List<SkillType>();
    
    private Dictionary<SkillType, ISkill> skillInstances = new Dictionary<SkillType, ISkill>();
    private Dictionary<SkillType, float> skillCooldowns = new Dictionary<SkillType, float>();
    private SkillFactory skillFactory = new SkillFactory();

    // Components
    private PlayerStats stats;
    private Rigidbody2D body;
    private PlayerMovement movement;

    [Header("Skill Settings")]
    public float dashSpeedMultiplier = 3f;
    public float dashDuration = 0.3f;
    public GameObject fireballPrefab;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
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
        HandleSkillInput();
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

    private void HandleSkillInput()
    {
        // Double Jump is handled in PlayerMovement since it uses jump input
        
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

    public bool TryExecuteSkill(SkillType type)
    {
        if (!HasSkill(type) || skillCooldowns[type] > 0) 
            return false;

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
            
            Debug.Log($"Unlocked skill: {type}");
        }
    }

    public bool HasSkill(SkillType type) => unlockedSkills.Contains(type);
    public float GetCooldown(SkillType type) => skillCooldowns.ContainsKey(type) ? skillCooldowns[type] : 0f;
    
    // Public methods for skills to access player components
    public PlayerStats GetStats() => stats;
    public Rigidbody2D GetRigidbody() => body;
    public PlayerMovement GetMovement() => movement;
}