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
    public int[] quickSlot_Record = { -2, -2, -2, -2, -2, -2, -2, -2, -2, -2 };
    public int slotLevel_Record = 0;


    void Start()
    {
        LoadSkills();
        SettingQuickSlot();
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
    private void SettingQuickSlot()
    {
        quickSlot_Record = new int[] { 0, 1, -1, -2, -2, -2, -2, -2, -2, -2, -2 };//테스트, 서버 데이터 바인딩 부분
        slotLevel_Record = 1;//테스트
        quickSlot_Record[slotLevel_Record + 1] = -1;//슬롯 추가용{레벨별}
        // 수정된 부분: 배열의 내용을 보기 쉽게 출력합니다.
        Debug.Log("퀵슬롯 상태: " + string.Join(", ", quickSlot_Record));
    }
}

public class PlayerSkillRecord
{
    public int id;       // 스킬 ID
    public bool owned;   // 보유 여부
    public int count;    // 보유 개수 (조각 등)
    public int level;    // 스킬 레벨
    // 클라 런타임에서만 쓰는 필드
    [NonSerialized] public SkillDefinition def;
}


