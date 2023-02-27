using System;
using UnityEngine;

public class AggresiveEnemy : Enemy
{
    private float _attackRange = 10.0f;

    public void Update()
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
    
    void OnCollisionEnter(Collision collision)
    {
        collision.collider.GetComponent<HealthComponent>().HealthValue -= _damage;
        
        HandleDeath();
        Instantiate(_onSuccessParticlePrefab, transform.position, transform.rotation);
    }
}
