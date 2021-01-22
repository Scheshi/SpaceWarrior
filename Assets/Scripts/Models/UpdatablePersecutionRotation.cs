using Asteroids.Interfaces;
using System;
using UnityEngine;


namespace Asteroids
{
    internal sealed class UpdatablePersecutionRotation : IRotation, IFrameUpdatable, IDisposable
    {
        private Transform _transform; 
        private Transform _playerTransform;
        private GameController _game;

        public UpdatablePersecutionRotation(Transform transform, Transform playerTransform, GameController gameController)
        {
            _playerTransform = playerTransform;
            _transform = transform;
            _game = gameController;
            _game.AddUpdatable(this);
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
            _game.RemoveUpdatable(this);
        }
    }
}
