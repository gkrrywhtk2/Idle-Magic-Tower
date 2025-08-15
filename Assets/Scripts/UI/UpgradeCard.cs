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



    public void CardUpdate()
    {
        Update_GoldCostText();
        Update_StatText();
        Update_Lvtext();
    }

    public void Update_GoldCostText()
    {
        var data = GameManager.instance.towerData;
        costText.text = UnitFormatter.FormatWithUnit(GetGoldCost(id, data.statLevels[id]));
    }
    private void Update_StatText()
    {
        var data = GameManager.instance.towerData;
        descText.text = GetStatPoint(id, data.statLevels[id]).ToString() +
            " -> " + GetStatPoint(id, data.statLevels[id] + 1).ToString();
    }
    private void Update_Lvtext()
    {
        var data = GameManager.instance.towerData;
        lvText.text = "Lv." + data.statLevels[id];
    }

    public void LevelUpStat()
    {
        //스탯을 레벨업 해주는 메서드

        var data = GameManager.instance.towerData;
        var pool = PoolingManager.instance.uIEffectPooling;

        //만약 보유 골드가 부족하면 오류 메세지 띄우자
        int requireGold = GetGoldCost(id, data.statLevels[id]);
        if (data.gold < requireGold)
        {
            UiManager.instance.ShowWarning("골드가 부족합니다!");
            return;
        }
        data.SpendGold(GetGoldCost(id, data.statLevels[id]));//골드 차감
        data.statLevels[id]++;//스탯 성장

        RectTransform effect = pool.Get(0).GetComponent<RectTransform>();//업그레이드 이펙트
        // 2️⃣ 이펙트도 월드 좌표로 변경
        effect.position = upgradeEffectPoint.position;

        RangeUpdate();//범위 갱신
        CardUpdate();//UI 갱신
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
            case 1://공격 범위
                cost = (nowLv + 1) * 4;
                break;
            default:
                cost = nowLv + 2;
                break;
        }
        return cost;
    }

    private float GetStatPoint(int type, int nowLv)
    {
        float statPoint = 0;
        switch (type)
        {
            case 0:
                statPoint = nowLv * 2;
                break;
            case 1://Range
                statPoint = nowLv * 0.01f;
                break;
            default:
                statPoint = nowLv * 2;
                break;
        }
        return statPoint;
    }

    private void RangeUpdate()
    {
        //공격 범위 현재 스탯에 맞게 업그레이드
        var data = GameManager.instance.towerData;
        int baseRange = 1;
        float newRange = (data.statLevels[1] * 0.01f) + baseRange;
        GameManager.instance.tower.scaner_Tower.UpgradeRange(newRange);
    }

    public void OnPointerDown()
    {
        InvokeRepeating(nameof(LevelUpStat), 0.5f, 0.1f); // 0.3초마다 반복 실행
    }

    public void OnPointerUp()
    {
        CancelInvoke(nameof(LevelUpStat)); // 업그레이드 중단
    }
    
}
