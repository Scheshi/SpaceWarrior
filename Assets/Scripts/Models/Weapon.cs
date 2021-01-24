using Asteroids.Interfaces;
using Asteroids.ObjectPool;
using Asteroids.Services;
using System;
using UnityEngine;


namespace Asteroids
{
    internal class Weapon : IWeapon, IDisposable
    {
        private Transform _startPositionTransform;
        private BulletObjectPool _bulletPool;
        private Action _fireAction;
        private float _force;
        private float _damage;
        private const float _cooldown = 1.0f;
        private float _lastFireTime = 0.0f;
        

        public Weapon(Transform startPositionTransform, ref Action fireAction, float force, float damage)
        {
            _fireAction = fireAction;
            _startPositionTransform = startPositionTransform;
            _force = force;
            _damage = damage;
            _bulletPool = ServiceLocatorObjectPool.Get<BulletObjectPool>();
        }

        public void Fire()
        {
            if (_lastFireTime + _cooldown < Time.time)
            {
                _lastFireTime = Time.time;
                var bullet = _bulletPool.Get<Rigidbody2D>(_startPositionTransform.position, _damage);
                bullet.AddForce(_startPositionTransform.up * _force, ForceMode2D.Impulse);
            }
        }

        public void Dispose()
        {
            _fireAction -= Fire;
        }
    }
}