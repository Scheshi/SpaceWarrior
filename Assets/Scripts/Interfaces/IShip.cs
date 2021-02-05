namespace Asteroids.Interfaces
{
    internal interface IShip : IMove, IRotation
    {
        IWeapon Weapon { get; }
        void SetNewWeapon(IWeapon fire);
    }
}
