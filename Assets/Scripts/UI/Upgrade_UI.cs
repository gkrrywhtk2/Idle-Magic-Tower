using TMPro;
using UnityEngine;

public class Upgrade_UI : MonoBehaviour
{
    public TowerData_Server towerData_Server;
    public UpgradeCard[] upgradeCards;

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
    
}
