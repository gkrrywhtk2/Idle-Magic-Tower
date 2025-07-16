using UnityEngine;

public class EnemyScaner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D towerRigid;//타워 리지드바디
    Rigidbody2D rigid;
    public float speed;//적의 이동속도
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

  // Update is called once per frame
    private void FixedUpdate()
    {
        MoveToTower();
    }

    private void MoveToTower()
    {
        Vector2 moveVec = towerRigid.position - rigid.position;
        Vector2 nextVec = moveVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }
    private void OnEnable()
    {
        towerRigid = GameManager.instance.tower.GetComponent<Rigidbody2D>();
    }
}
