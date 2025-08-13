using UnityEngine;
using TMPro;
using GameSystem.DamageFormat;

public class moneyUI : MonoBehaviour
{
    public TMP_Text goldText;
    public TowerData towerDataServer;

    private void OnEnable()
    {
        if (towerDataServer != null)
        {
            towerDataServer.OnGoldChanged += UpdateGoldText;
            UpdateGoldText(towerDataServer.Gold); // 초기값 표시
        }
    }

    private void OnDisable()
    {
        if (towerDataServer != null)
        {
            towerDataServer.OnGoldChanged -= UpdateGoldText;
        }
    }

    private void UpdateGoldText(int currentGold)
    {
        goldText.text = UnitFormatter.FormatWithUnit(currentGold);
    }
}
