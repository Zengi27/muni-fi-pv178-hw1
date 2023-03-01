using System;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class RandomTower : Tower
{
    private Collider _target;
    private float _timer;
    
    public void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _attackRange, _enemyLayerMask);

        if (hitColliders.Length > 0)
        {
            if (_target == null || !TargetInRange(hitColliders, _target))
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

    private void HandleShoot(Transform target)
    {
        _timer += Time.deltaTime;

        if (_timer > _timeBetweenShots)
        {
            Random random = new Random();
            int randomNum = random.Next(100);

            if (randomNum < 20)
            {
                Instantiate(_projectilePrefab, _projectileSpawn.position, Quaternion.identity).Init(target);
                Instantiate(_projectilePrefab, _projectileSpawn.position, Quaternion.identity).Init(target);
                
                _timer -= _timeBetweenShots;
            }
            if (randomNum >= 20 && randomNum < 80)
            {
                Instantiate(_projectilePrefab, _projectileSpawn.position, Quaternion.identity).Init(target);

                _timer -= _timeBetweenShots;
            }
        }
    }
}
