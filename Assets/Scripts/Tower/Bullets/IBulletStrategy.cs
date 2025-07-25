using UnityEngine;

public interface IBulletStrategy
{
    void Fire(Vector3 direction);
    void Effect();
}