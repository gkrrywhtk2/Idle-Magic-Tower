using UnityEngine;
[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Object/SkillData")]

public class SkillData : ScriptableObject
{
    public enum SkillRank { Common, Advanced, Rare, Epic, Legendary, Mythic };
    public int id;//스킬 고유 번호
    public SkillRank rank;//스킬 등급
    public string name_Kor;//스킬 이름
    public string content_Kor;//스킬 설명
    public Sprite icon;
}
