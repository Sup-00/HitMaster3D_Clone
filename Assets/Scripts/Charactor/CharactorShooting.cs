using UnityEngine;
using UnityEngine.Events;

public class CharactorShooting : MonoBehaviour
{
    [SerializeField] private UnityEvent _shoot;

    private Camera _camera;
    private BulletPool _bulletPool;
    private GunHolder _gunHolder;
    private Gun _gun;
    private bool _canShoot = true;

    private void Awake()
    {
        _camera = Camera.main;
        _gunHolder = GetComponentInChildren<GunHolder>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _canShoot == true)
        {
            Shoot();
        }
    }

    private void CreateGun()
    {
        Instantiate(_gun, _gunHolder.transform);
    }

    private void Shoot()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit);

        Bullet bullet = _bulletPool.GetBullet();

        if (bullet != null)
        {
            _gun.Shoot(bullet, raycastHit.point, _gunHolder);
            _shoot?.Invoke();
        }
    }

    public void Init(Gun gun, BulletPool bulletPool)
    {
        _gun = gun;
        _bulletPool = bulletPool;
        CreateGun();
    }

    public void CanShoot(bool state)
    {
        _canShoot = state;
    }
}