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
    
    public void Init(Transform target)
    {
        _target = target;
    }

    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        collider.GetComponent<HealthComponent>().HealthValue -= _damage;
        Destroy(gameObject);
    }
}
