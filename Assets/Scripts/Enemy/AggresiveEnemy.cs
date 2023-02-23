using UnityEngine;

public class AggresiveEnemy : Enemy
{
    public void Init(EnemyPath path)
    {
        _movementComponent.Init(path, 5);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Castle")
        {
            collision.collider.GetComponent<HealthComponent>().HealthValue -= 40;
            HandleDeath();
        }
    }
}
