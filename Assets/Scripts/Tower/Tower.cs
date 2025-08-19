using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Tower : MonoBehaviour
{
    public Scaner_Tower scaner_Tower;
    public Transform firePoint;
    public GameObject center;
    public TowerData towerData;//체력 등 타워 데이터
    Animator animator;
    public Slider slider_Hp;//체력 슬라이드
    public TMP_Text towerHp_Text;//타워 체력 텍스트 100/100


    void Awake()
    {
        animator = GetComponent<Animator>();
        Init();//초기 데이터 세팅
    }

    public void FireTrigger()
    {
        animator.SetTrigger("Fire");
    }
    public void Init()
    {
        towerData.maxHp = 100;
        towerData.nowHp = towerData.maxHp;
    }

    public void Hit(float damage)
    {
        //Enemy로 부터 damage를 받는다.
        towerData.nowHp -= damage;
        if (towerData.nowHp < 0)
        {
            Debug.Log("게임 오버");
        }
    }
    void FixedUpdate()
    {
        float now = towerData.nowHp;
        float max = towerData.maxHp;

        slider_Hp.value = now / max;
        towerHp_Text.text = $"{(int)now}/{(int)max}";
    }
    public float DamageCalculator(int baseDamage, out bool isCritical)
    {
        int baseATK = towerData.statLevels[(int)TowerData_Server.StatType.Attack];
        float randomFactor = UnityEngine.Random.Range(0.9f, 1.1f);
        int damage = Mathf.RoundToInt(baseDamage * baseATK * randomFactor);

        if (CriChanceCalculator())
        {
            isCritical = true;
            return CriHitCalculator(damage);
        }
        else
        {
            isCritical = false;
            return damage;
        }
    }
    public bool CriChanceCalculator()//크리티컬 확률 계산기
    {
        // 크리티컬 확률 (%) : 스탯 레벨 * 0.1f → 예: 레벨 50 = 5%
        float criPer = towerData.statLevels[(int)TowerData_Server.StatType.CritChance] * 0.1f;

        // 0~100 난수 생성
        float ran = Random.Range(0f, 100f);

        // 크리티컬 확률이 난수보다 크거나 같으면 성공
        return criPer >= ran;
    }
    public float CriHitCalculator(float damage)
    {
        int critPercent = towerData.statLevels[(int)TowerData_Server.StatType.CritDamage];
        float multiplier = 1f + (critPercent / 100f); // 100이면 x2
        return damage * multiplier; // 기본 데미지 + 크리 보너스 합산
    }
    


}
