using UnityEngine;

public class BaseBullet : MonoBehaviour, IBullet
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
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

    void OnEnable()
    {
        // 재화성화 시 애니메이션을 처음부터 재생
        animator.Play("BulletIdle", 0, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return; //Enemy Tag가 아니라면 리턴

        this.gameObject.SetActive(false);
    }
}
