using Asteroid.Interfaces;
using Asteroid.ObjectPool;
using Asteroids;
using System;
using UnityEngine;


namespace Asteroid.Views
{
    class EnemyShip : MonoBehaviour, IDamagable, IDisposable, IEnemy
    {
        private Health _health;
        private IDisposable _move;

        public void Damage(float point)
        {
            _health.Damage(point);
        }

        public void Dispose()
        {
            Destroy(gameObject);
            _move.Dispose();
        }

        public void Death()
        {
            EnemyObjectPool.ReturnToPool(this);
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
