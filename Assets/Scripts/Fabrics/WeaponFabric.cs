using Asteroid.Interfaces;
using Asteroids;
using System;
using UnityEngine;

namespace Asteroid.Fabrics
{
    internal sealed class WeaponFabric : IWeaponFabric
    {
        private BulletData _bulletData;

        public WeaponFabric(BulletData bulletData)
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
