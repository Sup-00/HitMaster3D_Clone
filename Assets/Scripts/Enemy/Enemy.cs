using UnityEngine;

[RequireComponent(typeof(Animator), typeof(CapsuleCollider))]
public class Enemy : MonoBehaviour
{
    private Animator _animator;
    private Platform _platform;
    private CapsuleCollider _collider;

    private void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
        _animator = GetComponent<Animator>();
    }

    public void Kill()
    {
        _animator.enabled = false;
        _collider.enabled = false;
        _platform.DeliteEnemyFromList(this);
        this.enabled = false;
    }

    public void Init(Platform platform)
    {
        _platform = platform;
    }
}