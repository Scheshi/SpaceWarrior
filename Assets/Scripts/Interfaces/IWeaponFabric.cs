using Asteroids;


namespace Asteroids.Interfaces
{
    interface IWeaponFabric
    {
        IWeapon Create(BarrelMarker barrel);
    }
}
