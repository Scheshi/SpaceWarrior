using Asteroids.Interfaces;
using System;
using UnityEngine;


namespace Asteroids
{
    internal sealed class UpdatablePersecutionRotation : IRotation, IFrameUpdatable, IDisposable
    {
        private Transform _transform; 
        private Transform _playerTransform;

        public UpdatablePersecutionRotation(Transform transform, Transform playerTransform)
        {
            _playerTransform = playerTransform;
            _transform = transform;
            GameController.AddUpdatable(this);
        }

        public void Update()
        {
            Rotation(_playerTransform.position - _transform.position);
        }

        public void Rotation(Vector3 direction)
        {
            _transform.up = direction;
        }

        public void Dispose()
        {
            GameController.RemoveUpdatable(this);
        }
    }
}
