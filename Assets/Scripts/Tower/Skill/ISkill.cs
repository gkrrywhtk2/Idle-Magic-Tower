using UnityEngine.EventSystems;

public interface ISkill
{
    void Use();//스킬 사용
    bool CheckUsable();//스킬 조건 검사
}