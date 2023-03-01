using UnityEngine;

public class BasicProjectile : Projectile
{
    private void OnTriggerEnter(Collider collider)
    {
        TakeDamage(collider);
        Destroy(gameObject);

        Instantiate(_onHitParticleSystem, transform.position, transform.rotation);
    }

    private void TakeDamage(Collider collider)
    {
        if (collider.TryGetComponent<HealthComponent>(out var healthComponent))
        {
            healthComponent.HealthValue -= _damage;
        }
    }
}
