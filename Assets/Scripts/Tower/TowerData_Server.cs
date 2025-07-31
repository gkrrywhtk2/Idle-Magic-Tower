using UnityEngine;

public class TowerData_Server : MonoBehaviour
{
    [Header("성장-강화 레벨")]
    public int attackLevel;
    public int rangeLevel;
    public int critChanceLevel;
    public int critDamageLevel;
    public int maxHpLevel;
    public int regenLevel;

    void Start()
    {
        Test_AllLevel1();
    }

  private void Test_AllLevel1()
    {
        //테스트용 일단 모두 레벨 1로
        attackLevel = 0;
        rangeLevel = 0;
        critChanceLevel = 0;
        critDamageLevel = 0;
        maxHpLevel = 0;
        regenLevel = 0;
    }
}
