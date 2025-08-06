using UnityEngine;

public class TowerData : MonoBehaviour
{
    public float maxHp;
    public float nowHp;

    public int[] statLevels;


    // 서버 데이터 복사 메서드
    public void CopyFromServer(TowerData_Server serverData)
    {
        for (int i = 0; i < statLevels.Length; i++)
        {
            statLevels[i] = serverData.statLevels[i];
        }
    }
    

}
