using UnityEngine;


public class SkillButton : MonoBehaviour
{
    public SkillManager skillManager;
    public int buttonIndex;
    public int nowSkillIndex;//현재 할당된 스킬 인덱스


    

    public void OnTouch()
    {
        if (skillManager.skillAbility.ContainsKey(nowSkillIndex))
        {
            skillManager.skillAbility[nowSkillIndex].Use(); // 해당 카드의 효과 실행
        }
        else
        {
            Debug.LogWarning("카드 효과가 정의되지 않았습니다.");
        }
    }
}
