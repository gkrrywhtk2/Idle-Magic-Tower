using UnityEngine;

public class Skill1_Heal : MonoBehaviour,ISkill
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Use()
    {
        Debug.Log("회복");
    }
}
