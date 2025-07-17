using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public static PoolingManager instance;

    [Header("#Connect")]
    public BulletPooling bulletPooling;

    void Start()
    {
        instance = this;
    }
}
