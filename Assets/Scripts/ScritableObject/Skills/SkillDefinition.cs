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

