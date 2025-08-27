using UnityEngine;

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
    public int[] statLevels = new int[(int)StatType.Count];

    // ✅ 재화
    public int gold;
    public int dia;

    // ✅ 스킬
    public SkillData_Server[] skillDatas;
}
public class SkillData_Server
{
    public int id;       // 스킬 ID
    public bool owned;   // 보유 여부
    public int count;    // 보유 개수 (조각 등)
    public int level;    // 스킬 레벨
}