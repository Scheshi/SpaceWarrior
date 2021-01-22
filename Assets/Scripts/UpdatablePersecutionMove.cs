using Asteroids.Interfaces;
using System;
using UnityEngine;


namespace Asteroids
{
    class UpdatablePersecutionMove : MoveTransform, IFrameUpdatable, IDisposable
    {
        public Action Stoping;
        private Transform _persecutionTransform;
        private Transform _transform;
        private GameController _game;

        public UpdatablePersecutionMove(Transform transform, Transform persecutionTransform, float speed, GameController gameController) : base(transform, speed)
        {
            _transform = transform;
            _persecutionTransform = persecutionTransform;
            _game = gameController;
            _game.AddUpdatable(this);
        }

        public void Update()
        {
            if ((_persecutionTransform.position - _transform.position).sqrMagnitude >= 2.0f)
            {
                Move(_persecutionTransform.position.x - _transform.position.x, _persecutionTransform.position.y - _transform.position.y, Time.deltaTime);
            }
            else Stoping?.Invoke();
        }

        public void Dispose()
        {
            _game.RemoveUpdatable(this);
        }
    }
}
