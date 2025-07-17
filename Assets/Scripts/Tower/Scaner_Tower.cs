using UnityEngine;

public class Scaner_Tower : MonoBehaviour
{
    [Header("Scan")]
    public float range;//공격 범위
    public LayerMask targetlayer;//감지할 레이어
    public RaycastHit2D[] targets;//감지된 타겟들
    public Transform mainTarget;//가장 가까운 타겟

    private void FixedUpdate()
    {

        targets = Physics2D.CircleCastAll(transform.position, range, Vector2.zero, 0, targetlayer);
        mainTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;

        foreach (RaycastHit2D target in targets)
        {
            Vector3 mypos = transform.position;
            Vector3 targetpos = target.transform.position;
            float curDiff = Vector3.Distance(mypos, targetpos);

            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }

        }
        return result;
    }
    
        private void OnDrawGizmosSelected()
    {
        // 색상 설정
        Gizmos.color = Color.red;

        // 현재 오브젝트 위치 기준으로 원형 범위 그리기
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
