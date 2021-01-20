using Asteroid.Interfaces;
using Asteroid.ObjectPool;
using Asteroids;
using System;
using UnityEngine;


namespace Asteroid.Views
{
    internal sealed class AsteroidEnemy : MonoBehaviour, IEnemy, IDisposable
    {
        private Health _health;


        public void Damage(float point)
        {
            _health.Damage(point);
        }

        public void InjectHealth(Health health)
        {
            if(_health == null)
            {
                _health = health;
            }
        }

        public void Death()
        {
            EnemyObjectPool.ReturnToPool(this);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.TryGetComponent<IDamagable>(out var damagable))
            {
                //TO-DO fixed hardcode
                damagable.Damage(10.0f);
                Death();
            }
        }
    }
}
