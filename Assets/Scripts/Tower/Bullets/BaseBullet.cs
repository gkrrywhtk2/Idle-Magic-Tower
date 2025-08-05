using UnityEngine;

public class BaseBullet : MonoBehaviour, IBulletStrategy
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        // 재화성화 시 애니메이션을 처음부터 재생
        //animator.Play("BulletIdle", 0, 0f);

        //3초후 비활성화(맵 밖으로 나간 경우)
        //Invoke("Disable", 3f);
    }

    public void Fire(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;

        // 1. 속도 설정 (방향으로 날아감)
        rb.linearVelocity = direction.normalized * speed;

        // 2. 회전 설정 (탄환이 방향을 바라보게)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Debug.Log("탄환이 몬스터를 향해 발사됨!");
    }
    public void Effect(Collider2D collision)
    {
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
        GameObject effect = PoolingManager.instance.bulletEffectPooling.Get(0);
        effect.transform.position = transform.position; //이펙트의 위치는 총알이 Effect()를 호출한 순간
        DamageText effectText = PoolingManager.instance.damageEffectPolling.Get(0).GetComponent<DamageText>();
        effectText.transform.position = enemy.damagePoint.position;
        effectText.Init(999);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("충돌 감지: " + collision.gameObject.name);
        if (!collision.CompareTag("Enemy"))
            return; //Enemy Tag가 아니라면 리턴
        //Debug.Log("충돌 감지: " + collision.gameObject.name);
        EnemyAI enemy = collision.GetComponent<EnemyAI>();
        enemy.CallHitStop();//피격 판정
        Effect(collision);//충돌 이펙트
        gameObject.SetActive(false);
       // Debug.Log("탄환이 사라졌습니다, 사유 : 충돌");
    }



    void Disable()
    {
        gameObject.SetActive(false);
        Debug.Log("탄환이 사라졌습니다, 사유 : 시간 종료");
    }
}
