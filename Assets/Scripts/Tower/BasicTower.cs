using System;
using UnityEngine;

public class BasicTower : Tower
{
    private float _timer;
    private Projectile _projectile;
    
    public void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _attackRange, _enemyLayerMask);

        if (hitColliders.Length > 0)
        {
            var target = FindNearestCollider(hitColliders).transform;
            
            _objectToPan.transform.LookAt(target);
            HandleShoot(target);
        }
    }

    public Collider FindNearestCollider(Collider[] colliders)
    {
        Collider nearestCollider = colliders[0];
        float nearestDistance = Vector3.Distance (transform.position, colliders[0].transform.position);
        
        foreach (var collider in colliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestCollider = collider;
            }
        }

        return nearestCollider;
    }

    private void HandleShoot(Transform target)
    {
        _timer += Time.deltaTime;

        
        if (_timer > _timeBetweenShots && _projectile == null)
        {
            _projectile = Instantiate(_projectilePrefab, _projectileSpawn.position, Quaternion.identity);
            _projectile.Init(target);
            _timer -= _timeBetweenShots;
        }
    }
}
