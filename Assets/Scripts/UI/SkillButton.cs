using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public SkillManager skillManager;
    public int buttonIndex;
    public int nowSkillIndex; // 현재 할당된 스킬 인덱스
    public Tower tower;

    // 쿨타임 관련
    public Image coolTimeBackGround;
    private float coolTime;      // 현재 스킬 쿨타임
    private float coolTimeTimer; // 경과 시간
    private bool isCooling;      // 쿨타임 중 여부
    public TMP_Text coolTimeText;

    void Start()
    {
        coolTimeText.text = "";//초기화
    }

    void Update()
    {
        if (!isCooling) return; // 쿨타임 중이 아니면 바로 리턴 → 최적화

        // 경과 시간 갱신
        coolTimeTimer += Time.deltaTime;
        float ratio = coolTimeTimer / coolTime;

        // 게이지 업데이트
        coolTimeBackGround.fillAmount = 1f - Mathf.Clamp01(ratio);

        // 남은 쿨타임 텍스트 (소수점 첫째자리)
        float remaining = Mathf.Max(coolTime - coolTimeTimer, 0f);
        coolTimeText.text = remaining.ToString("F1");

        // 쿨타임 종료
        if (coolTimeTimer >= coolTime)
        {
            isCooling = false;
            coolTimeBackGround.fillAmount = 0f;
            coolTimeText.text = ""; // 완료 시 텍스트 제거
        }
    }

    public void OnTouch()
    {
        var skill = skillManager.scriptableSkills[nowSkillIndex];

        // 쿨타임 중이면 실행 불가
        if (isCooling)
        {
            Debug.Log("스킬이 아직 쿨타임입니다");
            return;
        }

        // 실행 조건 검사
        bool useAble = skillManager.skillAbility[nowSkillIndex].CheckUsable();
        if (!useAble) return;

        // 마나 차감 후 스킬 발동
        tower.hpmpSystem.nowMp -= skill.cost;
        skillManager.skillAbility[nowSkillIndex].Use();

        // 쿨타임 시작
        coolTime = skill.coolTime;
        coolTimeTimer = 0f;
        isCooling = true;

        // fillAmount 초기화 → 꽉 찬 원에서 줄어드는 형태
        coolTimeBackGround.fillAmount = 1f;
        coolTimeText.text = coolTime.ToString("F1"); // 첫 프레임 표시
    }
}
