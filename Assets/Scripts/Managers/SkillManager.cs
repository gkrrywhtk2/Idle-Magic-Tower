using UnityEngine;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour
{
    public Dictionary<int, ISkill> skillAbility;
    public SkillDefinition[] scriptableSkills;


    void Start()
    {
        InitDictionary();
    }
    public void InitDictionary()
    {
        skillAbility = new Dictionary<int, ISkill>
        {
            { 0, gameObject.AddComponent<Skill0_MagicMissile>() },
            { 1, gameObject.AddComponent<Skill1_Heal>() },
        };
    }
}
