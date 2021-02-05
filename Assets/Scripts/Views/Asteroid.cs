using Asteroids.Interfaces;
using Asteroids.ObjectPool;
using System;
using Asteroids.Models;
using UnityEngine;
using Asteroids.Services;


namespace Asteroids.Views
{
    internal sealed class Asteroid : MonoBehaviour, IEnemy, IDisposable
    {
        private Health _health;
        private float _defence = 0.00f;
        private float _attack = 10.0f;

        public Health Health => _health;

        public float Defence
        {
            get => _defence;
            set
            {
                if (_defence - value >= 0 && _defence + value <= 1)
                {
                    _defence = value;
                }
            }
        }

        public float Attack
        {
            get => _attack;
            set
            {
                if (_defence - value >= 0 && _defence + value >= 0) _attack = value;
            }
        }

        public void Damage(float point)
        {
            _health.Damage(point - point * _defence);
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
            ServiceLocatorObjectPool.Get<EnemyObjectPool>().ReturnToPool(gameObject);
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
                damagable.Damage(_attack);
                Death();
            }
        }
    }
}
