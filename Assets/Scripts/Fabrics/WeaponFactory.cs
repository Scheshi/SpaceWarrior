using Asteroids.Interfaces;
using System;


namespace Asteroids.Fabrics
{
    internal sealed class WeaponFactory : IWeaponFabric
    {
        private BulletData _bulletData;

        public WeaponFactory(BulletData bulletData)
        {
            _bulletData = bulletData;
        }

        public IWeapon Create(BarrelMarker barrel, ref Action fireAction)
        {
            var bullet = new Weapon(
            _bulletData.Bullet,
            barrel.transform,
            ref fireAction,
            _bulletData.Force,
            _bulletData.Damage
            );

            fireAction += bullet.Fire;


            return bullet;
        }


    }
}
