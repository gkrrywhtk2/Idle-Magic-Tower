using UnityEngine;

public class RankColor : MonoBehaviour
{
    public Color[] rankColors = new Color[6];

    void Start()
    {
        // HEX 기반 색상 초기화
        rankColors = new Color[6];
        rankColors[(int)SkillDefinition.SkillRank.Common] = HexToColor("#FFFFFF"); // 흰색
        rankColors[(int)SkillDefinition.SkillRank.Advanced] = HexToColor("#1EFF00"); // 초록
        rankColors[(int)SkillDefinition.SkillRank.Rare] = HexToColor("#0070DD"); // 파랑
        rankColors[(int)SkillDefinition.SkillRank.Epic] = HexToColor("#A335EE"); // 보라
        rankColors[(int)SkillDefinition.SkillRank.Legendary] = HexToColor("#FF8000"); // 주황
        rankColors[(int)SkillDefinition.SkillRank.Mythic] = HexToColor("#FF0000"); // 빨강
    }
    private Color HexToColor(string hex)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hex, out color))
            return color;
        return Color.white;
    }
}
