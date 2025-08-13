using TMPro;
using UnityEngine;

public class WarningMessage : MonoBehaviour
{
    public TMP_Text warningMessage;

    public void ShowText(string warningMessageText)
    {
        warningMessage.text = warningMessageText;
    }
    public void ShowText_NotEnoughGold()
    {
        warningMessage.text = "골드가 부족합니다!";
    }
}
