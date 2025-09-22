using UnityEngine;

public class Skill1_Heal : MonoBehaviour, ISkill
{
    private Tower tower;
    private SkillManager skillManager;
    private int nowSkillIndex = 1;

    void OnEnable()
    {
        tower = GameManager.instance.tower;
        skillManager = GameManager.instance.skillManager;
    }
    public void Use()
    {
        Debug.Log("회복");
        float testUp = 10;
        tower.towerEffects.fastHeal.Play();//파티클 재생
        tower.hpmpSystem.ModifyHp(testUp);
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

        return true;
    }
    
}
