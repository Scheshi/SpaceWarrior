using Asteroids.Interfaces;
using Asteroids.ObjectPool;
using System;
using Asteroids.Models;
using UnityEngine;
using Asteroids.Services;


namespace Asteroids.Views
{
    internal sealed class Comet : MonoBehaviour, IEnemy, IDisposable
    {
        public event Action<string> ScoreUp;
        private Health _health;

        public Health Health => _health;

        public void Damage(float point)
        {
            _health.Damage(point);
        }

        
        public float Attack { get; set; }
        public float Defence { get; set; }

        public void InjectHealth(Health health)
        {
            if (_health == null)
            {
                _health = health;
            }
        }

        public void Death()
        {
            ServiceLocatorObjectPool.Get<EnemyObjectPool>().ReturnToPool(gameObject);
            ScoreUp?.Invoke("1000000");
        }
        public void Dispose()
        {
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<IDamagable>(out var damagable))
            {
                //TO-DO fixed hardcode
                damagable.Damage(Attack);
                Death();
            }
        }
    }
}