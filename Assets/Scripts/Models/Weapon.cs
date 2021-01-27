using Asteroids.Interfaces;
using Asteroids.ObjectPool;
using Asteroids.Services;
using System;
using UnityEngine;


namespace Asteroids
{
    internal class Weapon : IWeapon, IDisposable
    {
        private readonly Transform _startPositionTransform;
        private readonly BulletObjectPool _bulletPool;
        private readonly AudioClip _fireSound;
        
        private Action _fireAction;
        private float _force;
        private float _damage;
        private float _fireRate;
        private float _lastFireTime = 0.0f;
        

        public Weapon(Transform startPositionTransform, Action fireAction,
            AudioClip fireClip, float fireRate, float force, float damage)
        {
            _fireSound = fireClip;
            _fireAction = fireAction;
            _startPositionTransform = startPositionTransform;
            _fireRate = fireRate;
            _force = force;
            _damage = damage;
            _bulletPool = ServiceLocatorObjectPool.Get<BulletObjectPool>();
        }

        public void Fire()
        {
            if (_lastFireTime + _fireRate < Time.time)
            {
                AudioSource.PlayClipAtPoint(_fireSound, _startPositionTransform.position);
                _lastFireTime = Time.time;
                var bullet = _bulletPool.Get<Rigidbody2D>(_startPositionTransform.position, _damage, _startPositionTransform, null);
                bullet.AddForce(_startPositionTransform.up * _force, ForceMode2D.Impulse);
            }
        }

        public void Dispose()
        {
            _fireAction -= Fire;
        }
    }
}