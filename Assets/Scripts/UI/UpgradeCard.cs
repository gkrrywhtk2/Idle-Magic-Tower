using TMPro;
using UnityEngine;

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
        costText.text = GetGoldCost(id, data.statLevels[id]).ToString();
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
        //스탯 레벨업 함수
        var data = GameManager.instance.towerData;
        var pool = PoolingManager.instance.uIEffectPooling;
        data.statLevels[id]++;

        RectTransform effect = pool.Get(0).GetComponent<RectTransform>();
        // 2️⃣ 이펙트도 월드 좌표로 변경
        effect.position = upgradeEffectPoint.position;

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
            default:
                statPoint = nowLv * 2;
                break;
        }
        return statPoint;
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
