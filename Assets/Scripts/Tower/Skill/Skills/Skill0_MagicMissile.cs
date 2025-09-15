using TMPro;
using UnityEngine;

public class Skill0_MagicMissile : MonoBehaviour, ISkill
{
    private Tower tower;
    private SkillManager skillManager;
    private int nowSkillIndex = 0;

    void OnEnable()
    {
        tower = GameManager.instance.tower;
        skillManager = GameManager.instance.skillManager;
    }
    public void Use()
    {
        Fire();
    }
    public bool CheckUsable()
    {
        // 스킬 유효성 체크
        if (!skillManager.skillAbility.ContainsKey(nowSkillIndex))
        {
            Debug.LogWarning("카드 효과가 정의되지 않았습니다.");
            return false;
        }

        // 마나 체크
        var skill = skillManager.scriptableSkills[nowSkillIndex];
        if (tower.hpmpSystem.nowMp < skill.cost)
        {
            Debug.Log("마나가 부족합니다");
            return false;
        }
        if (tower.scaner_Tower.mainTarget == null)
        {
            Debug.Log("타켓이 없습니다");
            return false;
        }

        return true;
    }
    
    public void Fire()
    {

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
}
