using UnityEngine;

public class TouchZone : MonoBehaviour
{
    public Tower tower;
    private bool spawnTest = false;
    public GameObject enemyTest;

    void Awake()
    {
        // scaner_Tower = GameManager.instance.tower.scaner_Tower;

    }
    public void Fire()
    {
        if (tower.scaner_Tower.mainTarget == null) return; // 타겟 없으면 리턴

        tower.FireTrigger();//반짝임 애니메이션 연출

        BaseBullet bullet = PoolingManager.instance.bulletPooling.Get(0).GetComponent<BaseBullet>();
        bullet.transform.position = tower.firePoint.transform.position;

        // 콜라이더 중심으로 방향 설정
        Collider2D col = tower.scaner_Tower.mainTarget.GetComponent<Collider2D>();
        if (col != null)
        {
            Vector3 center = col.bounds.center;
            bullet.Fire(center); // 중심을 향해 발사
        }
        else
        {
            // 혹시 콜라이더 없으면 그냥 위치 사용
            bullet.Fire(tower.scaner_Tower.mainTarget.transform.position);
        }
    }
    public void SpawnTest()
    {
        if (spawnTest == true) return;
        enemyTest.SetActive(true);
        spawnTest = true;
    }
}
