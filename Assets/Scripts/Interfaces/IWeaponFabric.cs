using Asteroids;


namespace Asteroid.Interfaces
{
    interface IWeaponFabric
    {
        IWeapon Create(BarrelMarker barrel);
    }
}
