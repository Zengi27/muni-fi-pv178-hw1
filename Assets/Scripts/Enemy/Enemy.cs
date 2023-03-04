 using System;
using System.Collections;
using System.Collections.Generic;
 using Unity.VisualScripting;
 using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MovementComponent), typeof(HealthComponent), typeof(BoxCollider))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected int _reward;
    [SerializeField] protected int _damage;
    [SerializeField] protected MovementComponent _movementComponent;
    [SerializeField] protected HealthComponent _healthComponent;
    [SerializeField] protected ParticleSystem _onDeathParticlePrefab;
    [SerializeField] protected ParticleSystem _onSuccessParticlePrefab;
    [SerializeField] protected LayerMask _attackLayerMask;

    public event Action OnDeath;

    private void Start()
    {
        _healthComponent.OnDeath += HandleDeath;
        _movementComponent.MoveAlongPath();
    }

    private void OnDestroy()
    {
        _healthComponent.OnDeath -= HandleDeath;
    }

    public void Init(EnemyPath path)
    {
        _movementComponent.Init(path, _speed);
    }

    protected void HandleDeath()
    {
        GameObject.FindObjectOfType<Player>().Resources += _reward;
        OnDeath?.Invoke();
        Destroy(gameObject);
        //Instantiate(_onDeathParticlePrefab, transform.position, transform.rotation);
    }

    public abstract void Move();

    public abstract void TakeDamage(Collider collider);
}
