using UnityEngine;
using UnityEngine.UI;

public class SkillIcon_InfoButton : MonoBehaviour {
    private Button button;

    void Start()
    {
            button = GetComponent<Button>();
            // 버튼 클릭 시 UIManager의 OpenSkillInfo 함수를 호출하도록 리스너 추가
            button.onClick.AddListener(OnButtonClicked);
    } 
    

    /// <summary>
    /// 버튼이 클릭되었을 때 호출되는 메인 함수입니다.
    /// </summary>
    void OnButtonClicked()
    {
        // 어디서든 접근 가능한 UIManager의 Instance를 통해 함수 호출
        UiManager.instance.skillUI.OpenSkillInfo();
    }

        // 버튼 동작 후 UI를 새로고침해야 할 수 있습니다.
        // 예시: skillInfoPanel.RefreshUI();
    

    void OnDestroy()
    {
        // 오브젝트 파괴 시 리스너를 제거하여 메모리 누수를 방지합니다.
        if (button != null)
        {
            button.onClick.RemoveListener(OnButtonClicked);
        }
    }


}

    