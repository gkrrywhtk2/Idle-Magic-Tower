using UnityEngine;

public class ObjectController : MonoBehaviour
{
    // 애니메이션 이벤트에서 호출할 함수
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
