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
        private WeaponData _weaponData;
        private Action _fireAction;
        private float _force;
        private float _damage;
        private float _fireRate;
        private float _lastFireTime = 0.0f;
        
        #endregion
        


        public Weapon(Transform startPositionTransform, Action fireAction,
            WeaponData weaponData, float force, float damage)
        {
            _weaponData = weaponData;
            _fireSound = weaponData.FireClip;
            _fireAction = fireAction;
            _startPositionTransform = startPositionTransform;
            _fireRate = weaponData.FireRate;
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

        public void SetNewFireSound(AudioClip fireClip)
        {
            _fireSound = fireClip;
        }

        public void SetNewBarrelPosition(Transform barrel)
        {
            _startPositionTransform = barrel;
        }

        public void SetNewForce(float force)
        {
            _force = force;
        }

        public void SetNewFireRate(float fireRate)
        {
            _fireRate = fireRate;
        }

        public void SetNewDamage(float damage)
        {
            _damage = damage;
        }

        public void ResetFireRate()
        {
            _fireRate = _weaponData.FireRate;
        }

        public void ResetFireSound()
        {
            _fireSound = _weaponData.FireClip;
        }
    }
}