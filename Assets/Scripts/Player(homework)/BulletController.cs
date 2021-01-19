using Asteroid.Interfaces;
using UnityEngine;


namespace Asteroids
{
    internal class BulletController : IWeapon
    {
        private Rigidbody2D _bullet;
        private Transform _startPositionTransform;
        private float _force;
        private float _damage;

        public BulletController(Rigidbody2D bullet, Transform startPositionTransform, float force, float damage)
        {
            _bullet = bullet;
            _startPositionTransform = startPositionTransform;
            _force = force;
            _damage = damage;
        }

        public void Fire()
        {
            var bullet = GameObject.Instantiate(_bullet, _startPositionTransform.position, Quaternion.identity);
            bullet.gameObject.AddComponent<Bullet>().Damage = _damage;
            bullet.AddForce(_startPositionTransform.up * _force, ForceMode2D.Impulse);
        }
    }
}