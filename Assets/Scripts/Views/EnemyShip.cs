using Asteroids.Interfaces;
using Asteroids.ObjectPool;
using Asteroids.Services;
using System;
using Asteroids.Models;
using UnityEngine;


namespace Asteroids.Views
{
    class EnemyShip : MonoBehaviour, IDamagable, IDisposable, IEnemy
    {
        public event Action<string> ScoreUp;
        [SerializeField] WeaponData _weaponData;
        private Health _health;
        private IDisposable _move;

        public WeaponData Weapon => _weaponData;
        public Health Health => _health;
        
        public float Attack { get; set; }
        public float Defence { get; set; }

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
