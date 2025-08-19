using TMPro;
using UnityEngine;
using GameSystem.DamageFormat;

public class DamageText : MonoBehaviour
{
    public TMP_Text damageText;
    public Color normalColor;
    public Color criColor;

    public void Init(float damage, bool cri)
    {
        damageText.color = cri ? criColor : normalColor;
        damageText.text = UnitFormatter.FormatWithUnit(damage);

        if (cri)
        {
            Debug.Log("크리티컬 데미지!");
        }
        else
        {
            Debug.Log("일반 데미지");
        }
    }

}
