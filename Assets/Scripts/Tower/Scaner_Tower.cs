using UnityEngine;

public class Scaner_Tower : MonoBehaviour
{
    [Header("Scan")]
    public float range;//공격 범위
    public LayerMask targetlayer;//감지할 레이어
    public RaycastHit2D[] targets;//감지된 타겟들
    public Transform mainTarget;//가장 가까운 타겟

    [Header("Visual")]//라인 렌더러
    public int circleSegments = 50; // 원을 구성할 선분 개수
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.loop = true; // 원이 닫히도록
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.positionCount = circleSegments;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = new Color(0.4f, 0.8f, 1f, 0.1f); // 연한 하늘색
        lineRenderer.endColor   = new Color(0.4f, 0.8f, 1f, 0.1f); // 끝은 투명하게
    }

    private void Start()
    {
        DrawCircle(range);
    }

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

    public void UpgradeRange(float newRange)
    {
        range = newRange;
        DrawCircle(range);
    }

        void DrawCircle(float radius)
    {
        for (int i = 0; i < circleSegments; i++)
        {
            float angle = i * 2 * Mathf.PI / circleSegments;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            lineRenderer.SetPosition(i, new Vector3(
                transform.position.x + x,
                transform.position.y + y,
                transform.position.z
            ));
        }
    }
    
    //     private void OnDrawGizmosSelected()
    // {
    //     // 색상 설정
    //     Gizmos.color = Color.red;

    //     // 현재 오브젝트 위치 기준으로 원형 범위 그리기
    //     Gizmos.DrawWireSphere(transform.position, range);
    // }
}
