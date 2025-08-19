using System.Collections;
using UnityEngine;
using UnityEngine.Android;

public class EnemyAI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody2D towerRigid;//타워 리지드바디
    Rigidbody2D rigid;
    BoxCollider2D col;

    public float speed;//적의 이동속도
    Animator anim;
    public bool nowHit = false;//Hit코루틴 중복실행 방지
    SpriteRenderer sprite;
    private Color originalColor;
    public Color hitColor;//레드(엔진에서 관리)

    [Header("Scan")]
    public float attackRange;//공격 범위
    public LayerMask targetLayer;//감지할 레이어
    public RaycastHit2D[] targets;//감지된 타겟(타워)
    public Transform center;

    [Header("Attack")]//공격 관련
    public bool canAttack;//공격가능: 타워가 공격 범위내로 들어왔음
    private const float attackCoolTime = 2f;//공격 쿨타임
    private Coroutine attackLoop;

    [Header("Data")]//변수들 모음
    public float maxHp = 1000;//최대 체력
    public float nowHp;//현재 채력

    [Header("Point")]//지점 모음
    public Transform damagePoint;//데미지가 출력되는 위치

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        originalColor = sprite.color;
    }
    void Init()
    {
        nowHp = maxHp;
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
    private void FixedUpdate()
    {
        MoveToTower();
    }
    private bool ScanTower()
    {
        //타워가 공격 범위내에 있으면 true값 반환
        targets = Physics2D.CircleCastAll(center.position, attackRange, Vector2.zero, 0, targetLayer);
        
        return targets.Length > 0;
    }

        private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center.position, attackRange);
    }

    private void MoveToTower()
    {
        if (nowHit == true)//피격중이면 이동 중지
            return;
        if (canAttack == true)//타워가 공격 범위 이내면 이동 중지
            return;
        if (anim.GetBool("Dead"))//사망 애니메이션중이라면 이동 중지
        return;

        Vector2 moveVec = towerRigid.position - rigid.position;
        Vector2 nextVec = moveVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }
    IEnumerator AttackLoop()
    {
        while (true)
        {
            if (canAttack) {
                Attack();
            }

            yield return new WaitForSeconds(attackCoolTime);
        }
    }
    void Attack()
    {
         // 현재 애니메이터가 공격 중이면 중복 트리거 방지
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            return;
        // 물리 이동 정지
        rigid.linearVelocity = Vector2.zero;
        rigid.angularVelocity = 0f;

        anim.SetTrigger("Attack");
        GameManager.instance.tower.Hit(1);
        Debug.Log("몬스터가 공격했습니다!");
    }
    void OnEnable()
    {
        Init();
        towerRigid = GameManager.instance.tower.center.GetComponent<Rigidbody2D>();
        anim.SetBool("Run", true);

        if (attackLoop == null)
            attackLoop = StartCoroutine(AttackLoop());

        StartCoroutine(TowerScanLoop());
    }
    public void CallHit(float damage)
    {
        StartCoroutine(Hit());
        DemageCheck(damage);
    }
    IEnumerator Hit()
    {

        if (nowHit) yield break; // 중복 실행 방지

        nowHit = true;

        sprite.color = hitColor;
        float hitTime = 0.1f;

        //hitTime이후 원 상태로 복구
        yield return new WaitForSeconds(hitTime);

        sprite.color = originalColor;
        nowHit = false;
    }
    void DemageCheck(float damage)
    {
        if (anim.GetBool("Dead"))
            return;

        nowHp -= damage;

        if (nowHp <= 0)
        {
            col.enabled = false;
            anim.SetBool("Dead", true);
        }
    }

    public void Death()
    {
        //애니메이션 이벤트에서 처리되는 함수
        this.gameObject.SetActive(false);
    }

    IEnumerator TowerScanLoop()
    {
        while (true)
        {
            bool newCanAttack = ScanTower();

            if (newCanAttack != canAttack)
            {
                canAttack = newCanAttack;
                anim.SetBool("Run", !canAttack);
            }

            yield return new WaitForSeconds(0.1f); // 0.1초 주기로 체크
        }
    }


    private void OnDisable()
    {//비활성화 될때 코루틴 초기화
        if (attackLoop != null)
        {
            StopCoroutine(attackLoop);
            attackLoop = null;
        }
        //사망시 초기화
        canAttack = false;
        nowHit = false;
        anim.SetBool("Dead", false);
        col.enabled = true;
        }
    
}
