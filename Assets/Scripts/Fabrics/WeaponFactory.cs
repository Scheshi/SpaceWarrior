using Asteroids.Interfaces;
using Asteroids;
using System;
using UnityEngine;

namespace Asteroids.Fabrics
{
    internal sealed class WeaponFactory : IWeaponFabric
    {
        private BulletData _bulletData;

        public WeaponFactory(BulletData bulletData)
        {
            _bulletData = bulletData;
        }

        public IWeapon Create(BarrelMarker barrel)
        {
            var bullet = new WeaponController(
            _bulletData.Bullet,
            barrel.transform,
            _bulletData.Force,
            _bulletData.Damage
            );

            return bullet;
        }
    }
}
