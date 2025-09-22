using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class HpMpSystem : MonoBehaviour
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
    public bool Hp_useRegen = true;       // 회복 기능 ON/OFF
    public float Hp_regenAmount = 0f;     // 초당 회복량
    public float Hp_regenInterval = 1f;   // 회복 주기 (초 단위)

    [Header("마나 수치")]
    public float nowMp;
    public float maxMp;
    public Slider slider_Mp;
    public TMP_Text text_nowMp;

    [Header("마나 회복 옵션")]
    public bool Mp_useRegen = true;       // 회복 기능 ON/OFF
    public float Mp_regenAmount = 0.1f;     // 0.1초당 회복량
    public float Mp_regenInterval = 0.1f;   // 회복 주기 (초 단위)

    void Awake()
    {
        Init();
    }
    void Init()
    {
        SetHp();
        SetMp();
        if (Hp_useRegen)
            StartCoroutine(Hp_RegenRoutine());
        if (Mp_useRegen)
            StartCoroutine(Mp_RegenRoutine());
    }
    void SetHp()
    {
        nowHp = maxHp;
    }

    void SetMp()
    {
        maxMp = 9;
        nowMp = 0;
    }
    void FixedUpdate()
    {
        slider_Hp.value = nowHp / maxHp;
        towerHp_Text.text = $"{(int)nowHp}/{(int)maxHp}";

        slider_Mp.value = nowMp / maxMp;
        text_nowMp.text = $"{(int)nowMp}";
    }

    public void HpUpdate()
    {
        //모든 스탯을 확인하고 최대체력을 세팅하는 함수
        const int baseHp = 100;//기초 체력
        float plusHp = data.statLevels[((int)TowerData_Server.StatType.MaxHp)] * d;//추가 체력 계산(스탯)
        float finalHp = baseHp + plusHp;
        maxHp = finalHp;
    }

    IEnumerator Hp_RegenRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Hp_regenInterval);

            if (nowHp < maxHp) // 풀피가 아닐 때만 회복
            {
            // 매우 좋은 수정입니다!
            ModifyHp(Hp_regenAmount);
            }
        }
}
    public void RegenUpdate()
    {
        //스탯 가져와서 현재 체력 재생량 계산 후 업데이트하는 함수 
        int statLevel_Regen = data.statLevels[(int)TowerData_Server.StatType.Regen];
        float finalRegenAmount = statLevel_Regen * 0.1f;

        Hp_regenAmount = finalRegenAmount;
    }

    public void HpAllUpdate()
    {
        //체력 시스템 관련 전부 최신화
        RegenUpdate();
        HpUpdate();
    }

    IEnumerator Mp_RegenRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Mp_regenInterval);

            if (nowMp < maxMp) // 풀피가 아닐 때만 회복
            {
                nowMp = Mathf.Min(nowMp + Mp_regenAmount, maxMp);
            }
        }
    }
    


        /// <summary>
    /// 체력을 변경합니다. 양수(+)는 회복, 음수(-)는 피해입니다.
    /// 변경된 체력의 실제 변화량을 반환합니다.
    /// </summary>
    /// <param name="amount">변경할 체력량</param>
    /// <returns>실제로 적용된 체력 변화량</html/returns>
    public float ModifyHp(float amount)
    {
        // 1. 변경 전 현재 체력을 기록
        float beforeHp = nowHp;

        // 2. 체력 변경 (amount가 음수면 피해, 양수면 회복)
        float newHp = nowHp + amount;

        // 3. Clamp 함수로 newHp가 0과 maxHp 사이를 벗어나지 않도록 보정
        nowHp = Mathf.Clamp(newHp, 0, maxHp);

        // 4. 실제 체력 변화량을 계산하여 반환 (UI에 피해량/회복량 띄우기 등에 유용)
        float actualChange = nowHp - beforeHp;
        return actualChange;
    }
}
