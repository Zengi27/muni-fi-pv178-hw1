using UnityEngine;

public class ExplosiveProjctile : Projectile
{
    private float _explosiveRange = 5.0f;
    
    private void OnTriggerEnter(Collider collider)
    {
        Collider[] hitColliders = Physics.OverlapSphere(collider.transform.position, _explosiveRange, _enemyLayerMask);

        foreach (var col in hitColliders)
        {
            if (col.TryGetComponent<HealthComponent>(out var healthComponent))
            {
                healthComponent.HealthValue -= _damage;
            }
        }

        Destroy(gameObject);
    }
}
