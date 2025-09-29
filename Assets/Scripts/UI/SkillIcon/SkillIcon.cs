using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    [Header("Skill Info")]
    public int skillId;                  // 스킬 ID
    public TMP_Text textName;            // 스킬 이름 표시
    public Image skillImage;             // 스킬 아이콘 이미지
    public Image rankImage;              // 스킬 등급 색상 표시

   


    [Header("Progress")]
    public Slider progressSlider;        // 스킬 보유량 게이지
    public TMP_Text progressText;        // 슬라이더 위의 텍스트 (예: 1 / 2)

    [Header("Button")]                  //스킬 버튼용 
    public Image lockSprite;
    public Image plusSprite;
    public SkillIcon_InfoButton skillIcon_InfoButton;



    void Start()
    {
   
    }

    /// <summary>
    /// 스킬 아이콘 초기화
    /// </summary>
    public void Init_SkillIcon(int id)
    {
        skillId = id;
        var skillDefs = GameManager.instance.skillManager.scriptableSkills;

        // 안전하게 배열 범위 체크
        if (id < 0 || id >= skillDefs.Length)
        {
            Debug.LogWarning($"SkillIcon.Init: skillId {id} is out of range!");
            return;
        }

        var def = skillDefs[id];
        textName.text = def.skillName.Kor;
        skillImage.sprite = def.icon;

        int rankIndex = (int)def.rank;
        rankImage.color = GameManager.instance.rankColor.rankColors[rankIndex];
      

        UpdateState();


    }
    public void Init_SkillButton(int id)
    {

        if (id == -2)
        {
            skillImage.gameObject.SetActive(false);
            lockSprite.gameObject.SetActive(true);
            plusSprite.gameObject.SetActive(false);
            return;
        }
        else if (id == -1)
        {
            skillImage.gameObject.SetActive(false);
            lockSprite.gameObject.SetActive(false);
            plusSprite.gameObject.SetActive(true);
            return;
        }
        else
        {
            skillId = id;
            var skillDefs = GameManager.instance.skillManager.scriptableSkills;
            var def = skillDefs[id];
            skillImage.sprite = def.icon;
            skillImage.gameObject.SetActive(true);
            lockSprite.gameObject.SetActive(false);
            plusSprite.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 스킬 보유 상태
    /// </summary>
    public void UpdateState()
    {
        var skillStates = GameManager.instance.towerData.skill_State;

        // 안전하게 범위 체크
        if (skillId < 0 || skillId >= skillStates.Count)
        {
            Debug.LogWarning($"SkillIcon.UpdateState: skillId {skillId} is out of range!");
            return;
        }

        var state = skillStates[skillId];
        int requiredAmount = state.level + 1;       // 레벨업에 필요한 조각 수
        int ownedAmount = state.count;              // 현재 보유량

        progressSlider.value = requiredAmount > 0 ? (float)ownedAmount / requiredAmount : 0f;
        progressText.text = $"{ownedAmount} / {requiredAmount}";
    }

    
    
    
}
