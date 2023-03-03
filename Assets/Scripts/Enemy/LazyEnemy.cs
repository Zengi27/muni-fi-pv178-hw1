using UnityEngine;

public class LazyEnemy : Enemy
{
    private const float _endOfTimer = 0.0f;
    private const float _movingTime = 5.0f;
    private const float _waitTime = 1.0f;
    private float _timer;
    
    public void Update()
    {
        LazyMove();
    }

    private void OnCollisionEnter(Collision collision)
    {
        TakeDamage(collision.collider);
        Instantiate(_onSuccessParticlePrefab, transform.position, transform.rotation);

        HandleDeath();
    }

    private void LazyMove()
    {
        _timer += Time.deltaTime;

        if (_movementComponent.IsMoving && _timer > _movingTime)
        {
            _movementComponent.CancelMovement();
            _timer -= _movingTime;
        }
        if (!_movementComponent.IsMoving && _timer > _waitTime)
        {
            _movementComponent.MoveAlongPath();
        }
    }

    private void TakeDamage(Collider collider)
    {
        if (collider.gameObject.GetComponent<Castle>())
        {
            collider.gameObject.GetComponent<HealthComponent>().HealthValue -= _damage;
        }
        if (collider.gameObject.GetComponent<Enemy>())
        {
            collider.gameObject.GetComponent<HealthComponent>().HealthValue -= _damage * 2;
        }
    }
}
