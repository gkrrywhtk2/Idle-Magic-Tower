using UnityEngine;
using System.Collections.Generic;
using System;

public class TowerData_Server : MonoBehaviour
{
    // ✅ 스탯
    public enum StatType
    {
        Attack,
        Range,
        CritChance,
        CritDamage,
        MaxHp,
        Regen,
        Count // 항상 마지막
    }
    public int[] statLevels_Record = new int[(int)StatType.Count];

    // ✅ 재화
    public int gold_Record;
    public int dia_Record;

    // ✅ 스킬 (SkillData_Server를 사용해야 맞음!)
    public List<PlayerSkillRecord> skill_Record = new List<PlayerSkillRecord>();


    void Start()
    {
            LoadSkills();
    }
    public void LoadSkills()
    {
        // 예시 1
        skill_Record.Add(new PlayerSkillRecord
        {
            id = 0,
            owned = true,
            count = 10,
            level = 2
        });

        // 예시 2
        skill_Record.Add(new PlayerSkillRecord
        {
            id = 1,
            owned = true,
            count = 5,
            level = 1
        });

        Debug.Log("스킬 데이터 추가 완료! 현재 개수: " + skill_Record.Count);
    }
}


