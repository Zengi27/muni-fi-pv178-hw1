using UnityEngine;

public class BasicProjectile : Projectile
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<HealthComponent>(out var healthComponent))
        {
            healthComponent.HealthValue -= _damage;
            Destroy(gameObject);
        }
    }
}
