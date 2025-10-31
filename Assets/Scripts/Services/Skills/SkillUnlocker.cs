using UnityEngine;

public class SkillUnlocker : MonoBehaviour
{
    [SerializeField] private SkillType skillToUnlock;
    [SerializeField] private string unlockMessage = "New Ability Acquired!";
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var skillManager = other.GetComponent<PlayerSkillManager>();
            if (skillManager != null && !skillManager.HasSkill(skillToUnlock))
            {
                skillManager.UnlockSkill(skillToUnlock);
                Debug.Log(unlockMessage);
                gameObject.SetActive(false);
            }
        }
    }
}