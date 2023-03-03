using UnityEngine;

public class BasicProjectile : Projectile
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Enemy>())
        {
            TakeDamage(collider);
            Instantiate(_onHitParticleSystem, transform.position, transform.rotation);
            
            HandleDeath();
        }
    }

    private void TakeDamage(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<HealthComponent>(out var healthComponent))
        {
            healthComponent.HealthValue -= _damage;
        }
    }
}
