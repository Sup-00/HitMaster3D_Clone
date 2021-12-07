using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _target;
    private float _bulletSpeed = 30f;
    private bool _isFlying = false;

    private void Update()
    {
        if (_isFlying)
            transform.position = Vector3.MoveTowards(transform.position, _target, _bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        _isFlying = false;

        if (other.GetComponent<Enemy>())
        {
            var enemy = other.GetComponent<Enemy>();
            if (enemy.isActiveAndEnabled == true)
                enemy.Kill();
        }
    }

    public void MoveToTarget(Vector3 targetPosition, Transform gunPosition)
    {
        transform.position = gunPosition.position;
        _target = targetPosition;
        gameObject.SetActive(true);
        _isFlying = true;
    }
}