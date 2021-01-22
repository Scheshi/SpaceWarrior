using Asteroids.Interfaces;
using Asteroids.ObjectPool;
using Asteroids.Services;
using System;
using UnityEngine;


namespace Asteroids.Views
{
    class EnemyShip : MonoBehaviour, IDamagable, IDisposable, IEnemy
    {
        private Health _health;
        private IDisposable _move;

        public Health Health => _health;

        public void Damage(float point)
        {
            _health.Damage(point);
        }

        public void Dispose()
        {
            Destroy(gameObject);
            if(!Equals(_move, null))
                _move.Dispose();
        }

        public void Death()
        {
            ServiceLocatorObjectPool.Get<EnemyObjectPool>().ReturnToPool(gameObject);
            if (!Equals(_move, null))
                _move.Dispose();
        }

        public void InjectHealth(Health health)
        {
            if(Equals(_health, null))
                _health = health;
        }

        public void InjectMovement(IMove move)
        {
            if (Equals(_move, null) && move is IDisposable)
                _move = move as IDisposable;
        }

    }
}
