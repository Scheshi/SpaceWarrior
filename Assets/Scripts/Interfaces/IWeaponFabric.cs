using System;

namespace Asteroids.Interfaces
{
    interface IWeaponFabric
    {
        IWeapon Create(BarrelMarker barrel, Action fireAction, WeaponData weaponData);
    }
}
