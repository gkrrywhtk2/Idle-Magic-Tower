using UnityEngine;

public class SkillUI : MonoBehaviour
{
    public SkillIcon skillIconPrefab; // 스킬 아이콘 프리팹
    public Transform layout;          // 스킬 아이콘 부모 오브젝트
    

    public void Init_SkillIcon()
    {
        // 기존 아이콘 제거 (중복 생성 방지)
        foreach (Transform child in layout)
        {
            Destroy(child.gameObject);
        }

        var data = GameManager.instance.skillManager.scriptableSkills;

        // 모든 스킬 아이콘 생성
        for (int i = 0; i < data.Length; i++)
        {
            SkillIcon icon = Instantiate(skillIconPrefab, layout);
            icon.Init_SkillIcon(i);
            
        }
    }
}
