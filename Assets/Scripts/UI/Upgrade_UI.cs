using TMPro;
using UnityEngine;

public class Upgrade_UI : MonoBehaviour
{
    public TowerData_Server towerData_Server;
    public UpgradeCard[] upgradeCards;
    public int upgradeMode = 1; // +1, +10, +100 선택 모드


    public void UpdateCards()
    {
        for (int i = 0; i < upgradeCards.Length; i++)
        {
            upgradeCards[i].CardUpdate();
        }

        //이펙트 버그 해결 
        GameObject effect = PoolingManager.instance.uIEffectPooling.Get(0);
        effect.gameObject.SetActive(false);

    }
        public void SetUpgradeMode(int value)
    {
        upgradeMode = value;
        for (int i = 0; i < upgradeCards.Length; i++)
        {
            upgradeCards[i].SetUpgradeMode(value);
        }
    }
    
}
