using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public static PoolingManager instance;

    [Header("#Connect")]
    public BulletPooling bulletPooling;
    public BulletEffectPooling bulletEffectPooling;

    void Start()
    {
        instance = this;
    }
}
