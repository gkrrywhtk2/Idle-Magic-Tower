using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody2D towerRigid;//타워 리지드바디
    Rigidbody2D rigid;
    public float speed;//적의 이동속도
    Enemy enemyCtrl;//본체
    Animator anim;

    [Header("Scan")]
    public float attackRange;//공격 범위
    public LayerMask targetLayer;//감지할 레이어
    public RaycastHit2D[] targets;//감지된 타겟(타워)

    [Header("Attack")]//공격 관련
    public bool canAttack;//공격가능: 타워가 공격 범위내로 들어왔음
    private const float attackCoolTime = 2f;//공격 쿨타임
    private Coroutine attackLoop;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        enemyCtrl = GetComponent<Enemy>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        MoveToTower();
    }
    private bool ScanTower()
    {
        //타워가 공격 범위내에 있으면 true값 반환
        targets = Physics2D.CircleCastAll(enemyCtrl.center.position, attackRange, Vector2.zero, 0, targetLayer);
        
        return targets.Length > 0;
    }

        private void OnDrawGizmosSelected()
    {
        if (enemyCtrl != null && enemyCtrl.center != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(enemyCtrl.center.position, attackRange);
        }
    }

    private void MoveToTower()
    {
        if (enemyCtrl.nowHit == true)//피격중이면 이동 중지
            return;
        if (canAttack == true)//타워가 공격 범위 이내면 이동 중지
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
        towerRigid = GameManager.instance.tower.center.GetComponent<Rigidbody2D>();
        anim.SetBool("Run", true);

        if (attackLoop == null)
            attackLoop = StartCoroutine(AttackLoop());

        StartCoroutine(TowerScanLoop());
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
    }
    
}
