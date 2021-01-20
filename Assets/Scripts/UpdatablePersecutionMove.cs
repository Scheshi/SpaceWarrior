using Asteroids.Interfaces;
using System;
using UnityEngine;


namespace Asteroids
{
    class UpdatablePersecutionMove : MoveTransform, IFrameUpdatable, IDisposable
    {
        public event Action Stoping; 
        private Transform _persecutionTransform;
        private Transform _transform;

        public UpdatablePersecutionMove(Transform transform, Transform persecutionTransform, float speed) : base(transform, speed)
        {
            _transform = transform;
            _persecutionTransform = persecutionTransform;
            GameController.AddUpdatable(this);
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
            GameController.RemoveUpdatable(this);
        }
    }
}
