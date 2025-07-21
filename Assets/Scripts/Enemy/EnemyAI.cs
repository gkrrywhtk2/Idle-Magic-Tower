using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody2D towerRigid;//타워 리지드바디
    Rigidbody2D rigid;
    public float speed;//적의 이동속도
    Enemy enemyCtrl;//본체

    [Header("Scan")]
    public float attackRange;//공격 범위
    public LayerMask targetLayer;//감지할 레이어
    public RaycastHit2D[] targets;//감지된 타겟(타워)

    [Header("Attack")]//공격 관련
    private bool canAttack;//공격가능: 타워가 공격 범위내로 들어왔음
    private const float attackCoolTime = 1f;//공격 쿨타임
    private Coroutine attackLoop;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        enemyCtrl = GetComponent<Enemy>();
    }

    private void FixedUpdate()
    {
        MoveToTower();
        canAttack = ScanTower();

    }
    private bool ScanTower()
    {
        //타워가 공격 범위내에 있으면 true값 반환
        targets = Physics2D.CircleCastAll(transform.position, attackRange, Vector2.zero, 0, targetLayer);
        return targets.Length > 0;
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
        Debug.Log("몬스터가 공격했습니다!");
    }
    private void OnEnable()
    {
        towerRigid = GameManager.instance.tower.center.GetComponent<Rigidbody2D>();

        //최초 공격 코루틴 1회 실행후 지속적 조건 체크
        if (attackLoop == null)
            attackLoop = StartCoroutine(AttackLoop());
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
