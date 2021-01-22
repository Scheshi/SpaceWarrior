using Asteroids.Interfaces;
using Asteroids.ObjectPool;
using System;
using UnityEngine;


namespace Asteroids
{
    internal class Weapon : IWeapon, IDisposable
    {
        private Rigidbody2D _bullet;
        private Transform _startPositionTransform;
        private Action _fireAction;
        private float _force;
        private float _damage;
        private const float _cooldown = 1.0f;
        private float _lastFireTime = 0.0f;

        public Weapon(Rigidbody2D bullet, Transform startPositionTransform, Action fireAction, float force, float damage)
        {
            _bullet = bullet;
            _startPositionTransform = startPositionTransform;
            _force = force;
            _damage = damage;
            _fireAction = fireAction;
            _fireAction += Fire;
        }

        public void Fire()
        {
            if (_lastFireTime + _cooldown < Time.time)
            {
                _lastFireTime = Time.time;
                var bullet = BulletObjectPool.GetBullet(_bullet.gameObject, _startPositionTransform.position, _damage);
                bullet.AddForce(_startPositionTransform.up * _force, ForceMode2D.Impulse);
            }
        }

        public void Dispose()
        {
            _fireAction -= Fire;
        }
    }
}