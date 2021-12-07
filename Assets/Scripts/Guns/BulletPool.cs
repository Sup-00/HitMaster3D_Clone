using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private int _poolSize = 20;
    [SerializeField] private Bullet _bulletPrefab;

    private List<Bullet> _pool;

    private void Start()
    {
        _pool = new List<Bullet>();
        
        for (int i = 0; i < _poolSize; i++)
        {
            Bullet createdBullet = (Instantiate(_bulletPrefab, transform));
            createdBullet.gameObject.SetActive(false);
            _pool.Add(createdBullet);
        }
    }

    public Bullet GetBullet()
    {
        return _pool.FirstOrDefault(bullet => bullet.gameObject.activeSelf == false);
    }
}
