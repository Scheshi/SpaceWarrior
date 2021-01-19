using System;
using UnityEngine;

namespace Asteroids
{
    internal class BulletController
    {
        private Rigidbody2D _bullet;
        private Transform _startPositionTransform;
        private float _force;

        public BulletController(Rigidbody2D bullet, Transform startPositionTransform, float force)
        {
            _bullet = bullet;
            _startPositionTransform = startPositionTransform;
            _force = force;
        }

        internal void Fire()
        {
            var bullet = GameObject.Instantiate(_bullet, _startPositionTransform.position, Quaternion.identity);
            bullet.AddForce(_startPositionTransform.up * _force, ForceMode2D.Impulse);
        }
    }
}