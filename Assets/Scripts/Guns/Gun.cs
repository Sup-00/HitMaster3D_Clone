using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public abstract void Shoot(Bullet bullet, Vector3 targetPosition, GunHolder gunHolder);
}