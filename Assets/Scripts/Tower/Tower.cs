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

}
