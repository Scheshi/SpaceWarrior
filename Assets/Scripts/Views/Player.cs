using Asteroid.Interfaces;
using Asteroids;
using System;
using UnityEngine;


internal sealed class Player : MonoBehaviour, IDisposable, IPlayer, IDamagable
{
    private Health _health;

    public void Damage(float point)
    {
        _health.Damage(point);
    }

    public void Dispose()
    {
        Destroy(gameObject);
    }

    public void InjectHealth(Health health)
    {
        if (_health == null)
        {
            _health = health;
        }
    }
}
