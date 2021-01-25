using Asteroids.Interfaces;
using Asteroids;
using UnityEngine;
using System;

namespace Asteroids.Models
{
    internal sealed class CometMove : IMove, IFrameUpdatable, IDisposable
    {
        private readonly GameController _game;
        private readonly IMove _move;

        private float _horizontal;
        private float _vertical;
        private float _deltaTime;

        public CometMove(IMove move, GameController gameController)
        {
            _move = move;
            _game = gameController;
            _game.AddUpdatable(this);
        }

        public float Speed => _move.Speed;

        public void Update()
        {
            _move.Move(_horizontal, _vertical, _deltaTime);
        }

        public void Move(float horizontal, float vertical, float deltaTime)
        {
            _horizontal = horizontal;
            _vertical = vertical;
            _deltaTime = deltaTime;
        }

        public void Dispose()
        {
            _game.RemoveUpdatable(this);
        }
    }
}
