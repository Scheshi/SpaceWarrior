using Asteroid.Interfaces;
using Asteroid.ObjectPool;
using UnityEngine;


namespace Asteroids
{
    internal class WeaponController : IWeapon
    {
        private Rigidbody2D _bullet;
        private Transform _startPositionTransform;
        private float _force;
        private float _damage;
        private const float _cooldown = 1.0f;
        private float _lastFireTime = 0.0f;

        public WeaponController(Rigidbody2D bullet, Transform startPositionTransform, float force, float damage)
        {
            _bullet = bullet;
            _startPositionTransform = startPositionTransform;
            _force = force;
            _damage = damage;
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
    }
}