using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public static PoolingManager instance;

    [Header("#Connect")]
    public BulletPooling bulletPooling;
    public BulletEffectPooling bulletEffectPooling;
    public UIEffectPooling uIEffectPooling;
    public DamageEffectPolling damageEffectPolling;

    void Start()
    {
        instance = this;
    }
}
