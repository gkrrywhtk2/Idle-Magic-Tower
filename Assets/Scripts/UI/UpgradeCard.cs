using TMPro;
using UnityEngine;

public class UpgradeCard : MonoBehaviour
{
    public TMP_Text descText;
    public TMP_Text costText;
    public TMP_Text lvText;
    public int id;



    public void CardUpdate()
    {
        Update_GoldCostText();
        Update_StatText();
        Update_Lvtext();
    }

    public void Update_GoldCostText()
    {
        var data = GameManager.instance.towerData_Server;
        costText.text = GetGoldCost(id, data.statLevels[id]).ToString();
    }
    private void Update_StatText()
    {
        var data = GameManager.instance.towerData_Server;
        descText.text = GetStatPoint(id,  data.statLevels[id]).ToString() +
            " -> " + GetStatPoint(id,  data.statLevels[id] + 1).ToString();
    }
    private void Update_Lvtext()
    {
        var data = GameManager.instance.towerData_Server;
        lvText.text = "Lv." + data.statLevels[id];
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
}
