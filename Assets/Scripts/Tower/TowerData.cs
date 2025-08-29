using UnityEngine;
using System;
using System.Collections.Generic;

public class TowerData : MonoBehaviour
{
    public int gold;
    public int dia;
    // 골드가 변할 때 발생하는 이벤트
    public event Action<int> OnGoldChanged;

    public int[] statLevels;
    //스킬 데이터 모음
    public SkillDefinition[] skillDef;//스크립터블 오브젝트들
    public List<PlayerSkillRecord> skill_State = new List<PlayerSkillRecord>();//런타임용


    // 서버 데이터 복사 메서드
    public void CopyFromServer(TowerData_Server serverData)
    {
        for (int i = 0; i < statLevels.Length; i++)
        {
            statLevels[i] = serverData.statLevels_Record[i];
        }
        gold = serverData.gold_Record;
        dia = serverData.dia_Record;

         // 스킬 복사 (참조가 아닌 새 객체 생성)
    skill_State = new List<PlayerSkillRecord>();
    foreach (var serverSkill in serverData.skill_Record)
    {
        PlayerSkillRecord newRecord = new PlayerSkillRecord
        {
            id = serverSkill.id,
            owned = serverSkill.owned,
            count = serverSkill.count,
            level = serverSkill.level,
            def = Array.Find(skillDef, s => s.id == serverSkill.id) // 런타임 SO 매핑
        };
        skill_State.Add(newRecord);
    }
                foreach (var s in skill_State)
        {
            Debug.Log($"ID:{s.id}, Owned:{s.owned}, Count:{s.count}, Level:{s.level}, Name:{s.def?.name_Kor}");
        }
    }
        public void MapSkills(SkillDefinition[] allSkillDefs, List<PlayerSkillRecord> playerSkills)
    {
        foreach (var record in playerSkills)
        {
            record.def = Array.Find(allSkillDefs, s => s.id == record.id);
        }
    }
    

    //골드 관련 메서드
    public int Gold
    {
        get => gold;
        set
        {
            if (gold != value)
            {
                gold = value;
                OnGoldChanged?.Invoke(gold);
            }
        }
    }

    // 골드 추가
    public void AddGold(int amount)
    {
        Gold += amount;
    }

    // 골드 차감
    public bool SpendGold(int amount)
    {
        if (gold < amount) return false;
        Gold -= amount;
        return true;
    }
    //기본 체력 세팅
   
    

}
