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

        public IWeapon Create(BarrelMarker barrel, Action fireAction)
        {
            var bullet = new Weapon(
            _bulletData.Bullet,
            barrel.transform,
            fireAction,
            _bulletData.Force,
            _bulletData.Damage
            );


            return bullet;
        }


    }
}
