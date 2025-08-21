using TMPro;
using UnityEngine;
using GameSystem.DamageFormat;

public class UpgradeCard : MonoBehaviour
{
    public TMP_Text descText;
    public TMP_Text costText;
    public TMP_Text lvText;
    public int id;
    public RectTransform upgradeEffectPoint;
    const int d = 10;

    public int upgradeMode = 1; // +1, +10, +100 선택 모드

    public void CardUpdate(int futureAmount = 0)
    {
        Update_GoldCostText(futureAmount);
        Update_StatText(futureAmount);
        Update_Lvtext();
    }

    public void Update_GoldCostText(int futureAmount = 0)
    {
        var data = GameManager.instance.towerData;
        int totalCost = 0;
        int nowLv = data.statLevels[id];

        // 미래 레벨에 대한 총 필요 골드 계산
        for (int i = 0; i < futureAmount; i++)
        {
            totalCost += GetGoldCost(id, nowLv + i);
        }

        if (futureAmount == 0)
            totalCost = GetGoldCost(id, nowLv);

        costText.text = UnitFormatter.FormatWithUnit(totalCost);
    }

    private void Update_StatText(int futureAmount = 0)
    {
        var data = GameManager.instance.towerData;
        int futureLv = data.statLevels[id] + futureAmount;

        switch (id)
        {
            case 2:
            case 3:
                descText.text = GetStatPoint(id, data.statLevels[id]).ToString() + "%" +
                    " -> " + GetStatPoint(id, futureLv).ToString() + "%";
                break;
            default:
                descText.text = GetStatPoint(id, data.statLevels[id]).ToString() +
                    " -> " + GetStatPoint(id, futureLv).ToString();
                break;
        }
    }

        private void Update_Lvtext()
    {
        var data = GameManager.instance.towerData;
        int currentLv = data.statLevels[id];
        lvText.text = $"Lv.{currentLv}";
    }

    public void LevelUpStat()
    {
        var data = GameManager.instance.towerData;
        var pool = PoolingManager.instance.uIEffectPooling;
        var tower = GameManager.instance.tower;

        int totalCost = 0;
        int nowLv = data.statLevels[id];

        // 총 필요한 골드 미리 계산
        for (int i = 0; i < upgradeMode; i++)
            totalCost += GetGoldCost(id, nowLv + i);

        // 골드 부족 시 종료
        if (data.gold < totalCost)
        {
            UiManager.instance.ShowWarning($"{upgradeMode}레벨 업그레이드에 골드가 부족합니다!");
            return;
        }

        // 골드 차감 & 레벨업 적용
        data.SpendGold(totalCost);
        data.statLevels[id] += upgradeMode;

        // 효과 & UI 갱신
        RectTransform effect = pool.Get(0).GetComponent<RectTransform>();
        effect.position = upgradeEffectPoint.position;

        RangeUpdate();
        CardUpdate(upgradeMode);
        tower.hpSystem.HpAllUpdate();

        if (id == (int)TowerData_Server.StatType.MaxHp)
        {
            tower.hpSystem.HpUpdate();
            tower.hpSystem.nowHp += tower.hpSystem.d * upgradeMode;
        }
    }

    private int GetGoldCost(int type, int nowLv)
    {
        switch (type)
        {
            case 0: return (nowLv + 1) * 2;      // 공격력
            case 1: return (nowLv + 1) * 4;      // 범위
            default: return nowLv + 2;           // 기타
        }
    }

    private float GetStatPoint(int type, int nowLv)
    {
        switch (type)
        {
            case 0: return nowLv * 2;        // 공격력
            case 1: return nowLv * 0.1f;     // 범위
            case 2: return nowLv * 0.1f;     // 치명타 확률
            case 3: return nowLv * 1;        // 치명타 배율
            case 4: return nowLv * 10;       // 최대 체력
            case 5: return nowLv * 0.1f;     // 초당 체력 회복
            default: return nowLv * 2;
        }
    }

    private void RangeUpdate()
    {
        var data = GameManager.instance.towerData;
        int baseRange = 1;
        float newRange = (data.statLevels[1] * 0.01f) + baseRange;
        GameManager.instance.tower.scaner_Tower.UpgradeRange(newRange);
    }

    // ---------------------- 업그레이드 모드 설정 ----------------------
    public void SetUpgradeMode(int amount)
    {
        upgradeMode = amount;
        CardUpdate(upgradeMode); // 미리보기 UI 갱신
    }

    // ---------------------- 버튼 꾹 눌렀을 때 반복 업그레이드 ----------------------
    public void OnPointerDown()
    {
        InvokeRepeating(nameof(LevelUpStat_Repeat), 0.5f, 0.1f);
    }

    public void OnPointerUp()
    {
        CancelInvoke(nameof(LevelUpStat_Repeat));
    }

    public void LevelUpStat_Repeat()
    {
        LevelUpStat();
    }
}