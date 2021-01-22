using Asteroids.Interfaces;

namespace Asteroids.Fabrics
{
    internal class ShipFabric
    {
        private IMove _move;
        private IRotation _rotation;

        public ShipFabric(IMove moveTransform, IRotation rotation)
        {
            _move = moveTransform;
            _rotation = rotation;
        }

        public T Create<T>() where T: Ship
        {
            var ship = new Ship(_move, _rotation);
            return (T)ship;
        }
    }
}