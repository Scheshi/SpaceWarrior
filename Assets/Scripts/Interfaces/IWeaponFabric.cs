using System;

namespace Asteroids.Interfaces
{
    interface IWeaponFabric
    {
        IWeapon Create(BarrelMarker barrel, ref Action fireAction);
    }
}
