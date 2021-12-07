using UnityEngine;

public class Pistol : Gun
{
    public override void Shoot(Bullet bullet, Vector3 targetPosition, GunHolder gunHolder)
    {
        bullet.MoveToTarget(targetPosition, gunHolder.transform);
    }
}