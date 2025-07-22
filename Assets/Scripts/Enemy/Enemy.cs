using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public bool nowHit = false;//Hit코루틴 중복실행 방지
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    private Color originalColor;
    public Color hitColor;//레드(엔진에서 관리)
    private EnemyAI enemyAI;
    public Transform center;

    private int testHp;
    void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
    }

    void Init()
    {
        testHp = 10;
        InitCenterToRigid();
    }
    private void InitCenterToRigid()
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();
            // 콜라이더의 월드 중심을 가져와서
            Vector2 centerPos = col.bounds.center;
            // center Transform의 position에 설정
            center.position = centerPos;
          // Debug.LogWarning("Collider2D 또는 center Transform이 할당되지 않았습니다.", this);
    
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
