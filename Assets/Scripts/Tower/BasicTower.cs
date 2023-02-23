using System;
using UnityEngine;

public class BasicTower : Tower
{
    public void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _attackRange, _enemyLayerMask);

        if (hitColliders.Length > 0)
        {
            _objectToPan.transform.LookAt(FindNearestCollider(hitColliders).transform);
        }
    }

    public Collider FindNearestCollider(Collider[] colliders)
    {
        Collider nearestCollider = colliders[0];
        float nearestDistance = Vector3.Distance (transform.position, colliders[0].transform.position);
        
        foreach (var collider in colliders)
        {
            float distance = Vector3.Distance(transform.position, colliders[0].transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestCollider = collider;
            }
        }

        return nearestCollider;
    }
}