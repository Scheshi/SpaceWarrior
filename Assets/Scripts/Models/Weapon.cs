using Asteroids.Interfaces;
using Asteroids.ObjectPool;
using Asteroids.Services;
using System;
using UnityEngine;


namespace Asteroids
{
    internal class Weapon : IWeapon
    {
        #region Fields
        
        private Transform _startPositionTransform;
        private readonly BulletObjectPool _bulletPool;
        private AudioClip _fireSound;
        private Action _fireAction;
        private float _force;
        private float _damage;
        private float _fireRate;
        private float _lastFireTime = 0.0f;
        
        #endregion
        

        #region Constructors

        public Weapon(Transform startPositionTransform, Action fireAction,
            WeaponData weaponData, BulletData bulletData)
        {
            _fireSound = weaponData.FireClip;
            _fireAction = fireAction;
            _startPositionTransform = startPositionTransform;
            _fireRate = weaponData.FireRate;
            _force = bulletData.Force;
            _damage = bulletData.Damage;
            _bulletPool = ServiceLocatorObjectPool.Get<BulletObjectPool>();
        }

        #endregion
        
        #region Methods
        
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

        public void SetFireSound(AudioClip fireClip, out AudioClip oldClip)
        {
            oldClip = _fireSound;
            _fireSound = fireClip;
        }

        public void SetBarrelPosition(Transform barrel, out Transform oldBarrel)
        {
            oldBarrel = _startPositionTransform;
            _startPositionTransform = barrel;
        }

        public void SetForce(float force)
        {
            _force += force;
        }

        public void SetFireRate(float fireRate)
        {
            _fireRate += fireRate;
        }

        public void SetDamage(float damage)
        {
            _damage += damage;
        }

        #endregion
    }
}