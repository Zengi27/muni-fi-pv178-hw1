using System;
using UnityEngine;

public class BurstTower : Tower
{
    private float _timer;
    private float _secondBulletTimer;
    private int Shot;


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

        if (Shot == 1 && (_secondBulletTimer += Time.deltaTime) > 0.2f)
        {
            var _firstProjectile = Instantiate(_projectilePrefab, _projectileSpawn.position, Quaternion.identity);
            _firstProjectile.Init(target);
            Shot = 0;
        }
        
        if (_timer > _timeBetweenShots && Shot == 0)
        {
            var _firstProjectile = Instantiate(_projectilePrefab, _projectileSpawn.position, Quaternion.identity);
            _firstProjectile.Init(target);
            Shot = 1;
            _timer -= _timeBetweenShots;
            _secondBulletTimer = 0;
        }
        
    }
}
