using UnityEngine;

public class LazyEnemy : Enemy
{
    private const float _endOfTimer = 0.0f;
    private const float _movingTime = 5.0f;
    private const float _waitTime = 1.0f;
    private float _timer = _movingTime;
    
    public void Update()
    {
        _timer -= Time.deltaTime;

        if (_movementComponent.IsMoving && _timer <= _endOfTimer)
        {
            _movementComponent.CancelMovement();
            _timer += _movingTime + _waitTime;
        }

        if (!_movementComponent.IsMoving && _timer <= 5.0f)
        {
            _movementComponent.MoveAlongPath();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Castle")
        {
            collision.collider.GetComponent<HealthComponent>().HealthValue -= 25;
        }
        else
        {
            collision.collider.GetComponent<HealthComponent>().HealthValue -= 50;
        }
        
        HandleDeath();
    }
}
