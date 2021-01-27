using Asteroids.Interfaces;
using System;
using Asteroids.Models;


namespace Asteroids.Fabrics
{
    internal sealed class WeaponFactory : IWeaponFabric
    {
        private readonly BulletData _bulletData;

        public WeaponFactory(BulletData bulletData)
        {
            _bulletData = bulletData;
        }

        public IWeapon Create(BarrelMarker barrel, Action fireAction)
        {
            var weapon = new Weapon(
                barrel.transform,
            fireAction,
            _bulletData.Force,
            _bulletData.Damage
            );

            
            var locker = new WeaponLocker(weapon);
            
            return locker;
        }


    }
}
