using TMPro;
using UnityEngine;

public class Upgrade_UI : MonoBehaviour
{
    public TowerData_Server towerData_Server;

    [Header("성장-강화 레벨 텍스트")]
    public TMP_Text attackLevel_text;
    public TMP_Text rangeLevel_text;
    public TMP_Text critChanceLevel_text;
    public TMP_Text critDamageLevel_text;
    public TMP_Text maxHpLevel_text;
    public TMP_Text regenLevel_text;
    [Header("요구 골드량 텍스트")]
    public TMP_Text attackLevelUpCost_text;
    public TMP_Text rangeLevelUpCost_text;
    public TMP_Text critChanceLevelUpCost_text;
    public TMP_Text critDamageLevelUpCost_text;
    public TMP_Text maxHpLevelUpCost_text;
    public TMP_Text regenLevelUpCost_text;
    [Header("스탯 변화량 텍스트")]
    public TMP_Text attackLevelStat_text;
    public TMP_Text rangeLevelStat_text;
    public TMP_Text critChanceLevelStat_text;
    public TMP_Text critDamageLeveStat_text;
    public TMP_Text maxHpLevelStat_text;
    public TMP_Text regenLevelStat_text;

    public void Update_UpgradeUI()
    {
        Update_LvText();
        Update_GoldCostText();
        Update_StatPointText();
    }
    private void Update_LvText()
    {
        //레벨 텍스트만 현재 레벨에 맞게 초기화
        attackLevel_text.text = "Lv" + towerData_Server.attackLevel.ToString();
        rangeLevel_text.text = "Lv" + towerData_Server.attackLevel.ToString();
        critChanceLevel_text.text = "Lv" + towerData_Server.attackLevel.ToString();
        critDamageLevel_text.text = "Lv" + towerData_Server.attackLevel.ToString();
        maxHpLevel_text.text = "Lv" + towerData_Server.attackLevel.ToString();
        regenLevel_text.text = "Lv" + towerData_Server.attackLevel.ToString();
    }
    private void Update_GoldCostText()
    {
        //요구 골드량 현재 레벨에 맞게 초기화

        attackLevelUpCost_text.text = GetGoldCost(0, towerData_Server.attackLevel).ToString();
        rangeLevelUpCost_text.text = GetGoldCost(1, towerData_Server.attackLevel).ToString();
        critChanceLevelUpCost_text.text = GetGoldCost(2, towerData_Server.attackLevel).ToString();
        critDamageLevelUpCost_text.text = GetGoldCost(3, towerData_Server.attackLevel).ToString();
        maxHpLevelUpCost_text.text = GetGoldCost(4, towerData_Server.attackLevel).ToString();
        regenLevelUpCost_text.text = GetGoldCost(5, towerData_Server.attackLevel).ToString();

    }
    private int GetGoldCost(int type, int nowLv)
    {
        int cost = 0;
        //업그레이드에 필요한 골드량을 계산해주는 함수
        switch (type)
        {
            case 0://공격력
                cost = (nowLv + 1) * 2;
                break;
            default:
                cost = nowLv + 2;
                break;
        }
        return cost;
    }
    private void Update_StatPointText()
    {
        attackLevelStat_text.text = GetStatPoint(0, towerData_Server.attackLevel).ToString() +
            " -> " + GetStatPoint(0, towerData_Server.attackLevel + 1).ToString();
        rangeLevelStat_text.text = GetStatPoint(0, towerData_Server.attackLevel).ToString() +
            " -> " + GetStatPoint(0, towerData_Server.attackLevel + 1).ToString();
        critChanceLevelStat_text.text = GetStatPoint(0, towerData_Server.attackLevel).ToString() +
            " -> " + GetStatPoint(0, towerData_Server.attackLevel + 1).ToString();
        critDamageLeveStat_text.text = GetStatPoint(0, towerData_Server.attackLevel).ToString() +
            " -> " + GetStatPoint(0, towerData_Server.attackLevel + 1).ToString();
        maxHpLevelStat_text.text = GetStatPoint(0, towerData_Server.attackLevel).ToString() +
            " -> " + GetStatPoint(0, towerData_Server.attackLevel + 1).ToString();
        regenLevelStat_text.text = GetStatPoint(0, towerData_Server.attackLevel).ToString() +
            " -> " + GetStatPoint(0, towerData_Server.attackLevel + 1).ToString();
    }
    private float GetStatPoint(int type, int nowLv)
    {
        float statPoint = 0;
        switch (type)
        {
            case 0:
                statPoint = nowLv * 2;
                break;
            default:
                statPoint = nowLv * 2;
                break;
        }
        return statPoint;
    }
}
