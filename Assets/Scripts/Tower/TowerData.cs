using UnityEngine;
using System;

public class TowerData : MonoBehaviour
{
    public float maxHp;
    public float nowHp;
    public int gold;
    public int dia;
    // 골드가 변할 때 발생하는 이벤트
    public event Action<int> OnGoldChanged;

    public int[] statLevels;


    // 서버 데이터 복사 메서드
    public void CopyFromServer(TowerData_Server serverData)
    {
        for (int i = 0; i < statLevels.Length; i++)
        {
            statLevels[i] = serverData.statLevels[i];
        }
        gold = serverData.gold;
        dia = serverData.dia;

    }

    //골드 관련 메서드
     public int Gold
    {
        get => gold;
        set
        {
            if (gold != value)
            {
                gold = value;
                OnGoldChanged?.Invoke(gold);
            }
        }
    }

    // 골드 추가
    public void AddGold(int amount)
    {
        Gold += amount;
    }

    // 골드 차감
    public bool SpendGold(int amount)
    {
        if (gold < amount) return false;
        Gold -= amount;
        return true;
    }
    

}
