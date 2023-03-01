using System;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class RandomTower : Tower
{
    private Collider _target;
    private float _timer;
    private Projectile _projectile;
    
    public void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _attackRange, _enemyLayerMask);

        if (hitColliders.Length > 0)
        {
            if (_target == null && !TargetInRange(hitColliders, _target))
            {
                Random random = new Random();
                int randomNum = random.Next(hitColliders.Length);
                _target = hitColliders[randomNum];
            }

            _objectToPan.transform.LookAt(_target.transform);
            HandleShoot(_target.transform);
        }
        else
        {
            _target = null;
        }
    }

    private bool TargetInRange(Collider[] colliders, Collider target)
    {
        return colliders.Contains(target);
    }
    
    private void HandleShoot(Transform target)
    {
        _timer += Time.deltaTime;

        if (_timer > _timeBetweenShots && _projectile == null)
        {
            Random random = new Random();
            int randomNum = random.Next(100);

            if (randomNum < 20)
            {
                _projectile = Instantiate(_projectilePrefab, _projectileSpawn.position, Quaternion.identity);
                _projectile.Init(target);
                
                Instantiate(_projectilePrefab, _projectileSpawn.position, Quaternion.identity).Init(target);
                
                _timer -= _timeBetweenShots;
            }
            if (randomNum >= 20 && randomNum < 80)
            {
                _projectile = Instantiate(_projectilePrefab, _projectileSpawn.position, Quaternion.identity);
                _projectile.Init(target);
                _timer -= _timeBetweenShots;
            }
        }
    }
}
