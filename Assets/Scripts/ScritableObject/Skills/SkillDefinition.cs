using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Object/SkillDefinition")]

public class SkillDefinition : ScriptableObject
{
    public enum SkillRank { Common, Advanced, Rare, Epic, Legendary, Mythic };
    public int id;//스킬 고유 번호
    public SkillRank rank;//스킬 등급
    public string name_Kor;//스킬 이름
    public string content_Kor;//스킬 설명
    public Sprite icon;
    public int cost;//마나 비용
    public float coolTime;//재사용 대기시간
    


    public float baseDamage;       // 기본 공격력
    public float damagePerLevel;   // 레벨마다 추가되는 공격력  

    public float SetDamage(int skillLevel)
    {
        return baseDamage + (damagePerLevel * (skillLevel - 1));
    }
}

