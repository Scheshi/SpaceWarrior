using Asteroid.Interfaces;
using UnityEngine;

namespace Asteroids
{
    internal sealed class UpdatablePersecutionRotation : RotationShip, IFrameUpdatable
    {
        private Transform _transform; 
        private Transform _playerTransform;

        public UpdatablePersecutionRotation(Transform transform, Transform playerTransform) : base(transform)
        {
            _playerTransform = playerTransform;
            _transform = transform;
            GameController.AddUpdatable(this);
        }

        public void Update()
        {
            Rotation(_transform.position - _playerTransform.position);
        }

    }
}
