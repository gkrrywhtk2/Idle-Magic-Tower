using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool nowHit = false;//Hit코루틴 중복실행 방지
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    private Color originalColor;
    public Color hitColor;//레드(엔진에서 관리)

    private int testHp;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
        Init();
    }

    void Init()
    {
        testHp = 10;
    }
    void OnEnable()
    {
        Init();
    }

    public void CallHitStop()
    {
        StartCoroutine(Hit());
    }
    IEnumerator Hit()
    {

        if (nowHit) yield break; // 중복 실행 방지
        nowHit = true;

        DemageCheck();
        Vector3 towerPos = GameManager.instance.tower.center.transform.position;
        Vector3 dirvec = transform.position - towerPos;

        rigid.linearVelocity = Vector2.zero; // 속도 초기화
        rigid.AddForce(dirvec.normalized * 0.1f, ForceMode2D.Impulse);
        sprite.color = hitColor;
        float hitTime = 0.1f;

        //hitTime이후 원 상태로 복구
        yield return new WaitForSeconds(hitTime);

        sprite.color = originalColor;
        nowHit = false;
    }

    void DemageCheck()
    {
        testHp--;
        if (testHp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
