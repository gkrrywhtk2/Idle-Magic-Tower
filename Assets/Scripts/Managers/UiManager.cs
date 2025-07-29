using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [Header("#Connect_GameObject")]
    public GameObject UpgradeBoard;

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
}
