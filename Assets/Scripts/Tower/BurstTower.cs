using System;
using UnityEngine;

public class BurstTower : Tower
{
    private float _timer;
    private float _timeBetweenSecondShot = 0.2f;
    private Projectile _firstProjectile;
    private Projectile _secondProjectile;


    public void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _attackRange, _enemyLayerMask);

        if (hitColliders.Length > 0)
        {
            var target = FindEnemyWithMaxHealth(hitColliders).transform;
            
            _objectToPan.transform.LookAt(target);
            HandleShoot(target);
        }
    }

    private Collider FindEnemyWithMaxHealth(Collider[] colliders)
    {
        Collider colliderWithMaxHealth = colliders[0];
        int maxHealth = 0;

        foreach (var collider in colliders)
        {
            collider.TryGetComponent<HealthComponent>(out var healthComponent);
            int health = healthComponent.HealthValue;

            if (maxHealth > health)
            {
                maxHealth = health;
                colliderWithMaxHealth = collider;
            }
        }

        return colliderWithMaxHealth;
    }
    
    private void HandleShoot(Transform target)
    {
        _timer += Time.deltaTime;
        
        if (_timer > _timeBetweenShots && _firstProjectile == null)
        {
            _firstProjectile = Instantiate(_projectilePrefab, _projectileSpawn.position, Quaternion.identity);
            _firstProjectile.Init(target);
        
        }
        
        if (_firstProjectile != null && _secondProjectile == null && _timer > _timeBetweenShots + _timeBetweenSecondShot)
        {
            _secondProjectile = Instantiate(_projectilePrefab, _projectileSpawn.position, Quaternion.identity);
            _secondProjectile.Init(target);
            
            _timer -= _timeBetweenShots;
        }
    }
}
