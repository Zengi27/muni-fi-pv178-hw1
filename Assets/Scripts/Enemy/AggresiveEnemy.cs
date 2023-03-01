using System;
using UnityEngine;

public class AggresiveEnemy : Enemy
{
    private float _attackRange = 10.0f;

    public void Update()
    {
        Move();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        TakeDamage(collision.collider);
        HandleDeath();
        
        Instantiate(_onSuccessParticlePrefab, transform.position, transform.rotation);
    }

    private void TakeDamage(Collider collider)
    {
        if (collider.TryGetComponent<HealthComponent>(out var healthComponent))
        {
            healthComponent.HealthValue -= _damage;
        }
    }

    private void Move()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _attackRange, _attackLayerMask);

        if (hitColliders.Length > 0)
        {
            var target = hitColliders[0].transform;
            _movementComponent.MoveTowards(target);
        }
        else
        {
            _movementComponent.MoveAlongPath();
        }
    }
    
    
}
