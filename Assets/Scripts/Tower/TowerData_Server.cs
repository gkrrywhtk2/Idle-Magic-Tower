using UnityEngine;

public class TowerData_Server : MonoBehaviour
{
    public enum StatType
    {
        Attack,
        Range,
        CritChance,
        CritDamage,
        MaxHp,
        Regen,
        Count // 항상 마지막에 위치 (배열 길이용)
    }

    public int[] statLevels = new int[(int)StatType.Count];

    void Start()
    {
       // AllLevel0();
    }
    
    private void AllLevel0()
    {
        for (int i = 0; i < statLevels.Length; i++)
        {
            statLevels[i] = 0;
        }
    }


}
