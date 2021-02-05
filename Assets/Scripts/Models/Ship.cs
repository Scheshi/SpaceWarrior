using Asteroids.Interfaces;
using Asteroids.Models;
using UnityEngine;


namespace Asteroids
{
    internal class Ship : IShip
    {
        private readonly IMove _moveImplementation;
        private readonly IRotation _rotationImplementation;
        private IWeapon _weapon;

        public float Speed => _moveImplementation.Speed;
        public IWeapon Weapon => _weapon;

        public Ship(IMove moveImplementation, IRotation rotationImplementation)
        {
            _moveImplementation = moveImplementation;
            _rotationImplementation = rotationImplementation;
        }

        public void Move(float horizontal, float vertical, float deltaTime)
        {
            _moveImplementation.Move(horizontal, vertical, deltaTime);
        }

        public void Rotation(Vector3 direction)
        {
            _rotationImplementation.Rotation(direction);
        }

        public void SetNewWeapon(IWeapon weapon)
        {
            if (_weapon != null)
            {
                _weapon.Dispose();
            }
            _weapon = weapon;
        }

        public void AddAcceleration()
        {
            if (_moveImplementation is AccelerationMove accelerationMove)
            {
                accelerationMove.AddAcceleration();
            }
        }

        public void RemoveAcceleration()
        {
            if (_moveImplementation is AccelerationMove accelerationMove)
            {
                accelerationMove.RemoveAcceleration();
            }
        }
    }
}
