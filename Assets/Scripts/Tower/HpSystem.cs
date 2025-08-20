using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class HpSystem : MonoBehaviour
{
    [Header("데이터 연결")]
    public TowerData data;
    [Header("UI")]
    public Slider slider_Hp;           // 체력 슬라이드
    public TMP_Text towerHp_Text;      // 타워 체력 텍스트 (예: 100/100)

    [Header("체력 수치")]
    public float maxHp = 100;
    public float nowHp;
    public int d = 10;

    [Header("체력 회복 옵션")]
    public bool useRegen = true;       // 회복 기능 ON/OFF
    public float regenAmount = 0f;     // 초당 회복량
    public float regenInterval = 1f;   // 회복 주기 (초 단위)

    void Awake()
    {
        Init();
    }
    void Init()
    {
        SetHp();
        if (useRegen)
            StartCoroutine(RegenRoutine());
    }
    void SetHp()
    {
        nowHp = maxHp;
    }
    void FixedUpdate()
    {
        slider_Hp.value = nowHp / maxHp;
        towerHp_Text.text = $"{(int)nowHp}/{(int)maxHp}";
    }

    public void HpUpdate()
    {
        //모든 스탯을 확인하고 최대체력을 세팅하는 함수
        const int baseHp = 100;//기초 체력
        float plusHp = data.statLevels[((int)TowerData_Server.StatType.MaxHp)] * d;//추가 체력 계산(스탯)
        float finalHp = baseHp + plusHp;
        maxHp = finalHp;
    }

    IEnumerator RegenRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(regenInterval);

            if (nowHp < maxHp) // 풀피가 아닐 때만 회복
            {
                nowHp = Mathf.Min(nowHp + regenAmount, maxHp);
            }
        }
    }
    public void RegenUpdate()
    {
        //스탯 가져와서 현재 체력 재생량 계산 후 업데이트하는 함수 
        int statLevel_Regen = data.statLevels[(int)TowerData_Server.StatType.Regen];
        float finalRegenAmount = statLevel_Regen * 0.1f;

        regenAmount = finalRegenAmount;
    }

    public void HpAllUpdate()
    {
        //체력 시스템 관련 전부 최신화
        RegenUpdate();
        HpUpdate();
    }
}
