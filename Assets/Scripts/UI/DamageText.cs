using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using GameSystem.DamageFormat;

public class DamageText : MonoBehaviour
{
    public TMP_Text damageText;

    public void Init(int damage)
    {
        damageText.text = UnitFormatter.FormatWithUnit(damage);
    }

}
