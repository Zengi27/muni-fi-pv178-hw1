using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected int _damage;
    [SerializeField] protected int _speed;
    [SerializeField] protected float _timeDeath;
    [SerializeField] protected Rigidbody _rb;
    [SerializeField] protected LayerMask _enemyLayerMask;
    [SerializeField] protected ParticleSystem _onHitParticleSystem;

    protected Transform _target;
    private float _timer = 0.0f;
    
    public void Init(Transform target)
    {
        _target = target;
    }

    public void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _timeDeath || _target == null)
        {
            HandleDeath();
        }

        if (_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * 5.0f *Time.deltaTime);
        }
    }

    protected void HandleDeath()
    {
        Destroy(gameObject);
    }
}
