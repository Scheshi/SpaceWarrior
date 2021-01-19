namespace Asteroids
{
    internal class ShipInitializator
    {
        public ShipInitializator(InputManager inputManager, AccelerationMove moveTransform, RotationShip rotation)
        {
            var ship = new Ship(moveTransform, rotation);
            inputManager.Move += ship.Move;
            inputManager.AccelerateDown += ship.RemoveAcceleration;
            inputManager.AccelerateUp += ship.AddAcceleration;
            inputManager.Rotation += ship.Rotation;
        }
    }
}