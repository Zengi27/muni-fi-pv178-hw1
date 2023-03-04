using System;
using UnityEngine;

public class BurstTower : Tower
{
    private float _timer;
    private float _secondProjectileTimer;
    private float _timeBetweenSecondShot = 0.2f;
    private bool waitForSecondBullet = false;
    private Collider _target;

    public void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _attackRange, _enemyLayerMask);

        if (hitColliders.Length > 0)
        {
            if (_target == null || !TargetInRange(hitColliders, _target))
            {
                if (waitForSecondBullet)
                {
                    waitForSecondBullet = false;
                    _secondProjectileTimer = _timer;
                }
                else
                {
                    _target = FindEnemyWithMaxHealth(hitColliders);
                    _objectToPan.transform.LookAt(_target.transform);
                    HandleShoot(_target.transform);
                }
            }
            else
            {
                _objectToPan.transform.LookAt(_target.transform);
                HandleShoot(_target.transform);
            }    
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
        _secondProjectileTimer += Time.deltaTime;
        
        if (_timer > _timeBetweenShots)
        {
            Instantiate(_projectilePrefab, _projectileSpawn.position, Quaternion.identity).Init(target);

            waitForSecondBullet = true;
            _timer -= _timeBetweenShots;
        }
        if (_secondProjectileTimer > _timeBetweenShots + _timeBetweenSecondShot)
        {
            Instantiate(_projectilePrefab, _projectileSpawn.position, Quaternion.identity).Init(target);

            waitForSecondBullet = false;
            _secondProjectileTimer = _timer;
        }
    }
}
