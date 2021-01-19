namespace Asteroids
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

        public Ship Create()
        {
            var ship = new Ship(_move, _rotation);
            return ship;
        }
    }
}