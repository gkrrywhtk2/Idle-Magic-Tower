using TMPro;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    public bool earned;//획득 여부
    public int id;//스킬ID
    public Slider slider;//스킬 보유 게이지
    public TMP_Text text_Name;//이름 tmp_text
    public Image rankImage;//등급별 색상 변경용
    public Image skillImage;//스킬 아이콘 이미지
}
