using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [Header("#Connect_GameObject")]
    public GameObject UpgradeBoard;
    public WarningMessage warningMessage;

    void Start()
    {
        instance = this;
    }
    public void OpenBoard(int boardId)
    {
        //선택한 보드 메뉴를 여는 함수.
        switch (boardId)
        {
            case 0:
                UpgradeBoard.gameObject.SetActive(true);
                break;

        }
    }
    public void ShowWarning(string message)
    {
        //message를 경고창에 띄우는 메서드
        warningMessage.gameObject.SetActive(true);
        warningMessage.warningMessage.text = message;
    }
}
