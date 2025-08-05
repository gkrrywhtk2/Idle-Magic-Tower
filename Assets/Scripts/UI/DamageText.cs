using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DamageText : MonoBehaviour
{
    public TMP_Text damageText;

    public void Init(int damage)
    {
        damageText.text = damage.ToString();
    }

}
