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

        public void Damage(float point)
        {
            _health.Damage(point);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        public void Death()
        {
            EnemyObjectPool.ReturnToPool(this);
        }

        public void InjectHealth(Health health)
        {
            _health = health;
        }

    }
}
