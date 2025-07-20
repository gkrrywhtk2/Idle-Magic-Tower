using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GameManager instance;

    [Header("#Connect_GameObject")]
    public Tower tower; //타워 오브젝트 메인

    void Start()
    {
        instance = this;
        Application.targetFrameRate = 60;//60프레임 고정
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
