using System;
using System.Linq;
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
        Instantiate(_onSuccessParticlePrefab, transform.position, transform.rotation);
        
        HandleDeath();
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
        var target = FindTowerInRange();

        if (target != null)
        {
            _movementComponent.MoveTowards(target.transform);
        }
        else
        {
            _movementComponent.MoveAlongPath();
        }
    }

    private Collider FindTowerInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _attackRange, _attackLayerMask);

        foreach (var collider in hitColliders)
        {
            if (collider.gameObject.GetComponent<Tower>())
            {
                return collider;
            }
        }

        return null;
    }
}
