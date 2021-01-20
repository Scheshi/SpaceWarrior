using Asteroid.Interfaces;
using Asteroids;
using UnityEngine;

namespace Asteroid
{
    class UpdatablePersecutionMove : MoveTransform, IFrameUpdatable
    {
        Transform _playerTransform;
        Transform _transform;

        public UpdatablePersecutionMove(Transform transform, Transform playerTransform, float speed) : base(transform, speed)
        {
            _transform = transform;
            _playerTransform = playerTransform;
            GameController.AddUpdatable(this);
        }

        public void Update()
        {
            if((_playerTransform.position - _transform.position).sqrMagnitude >= 2.0f)
            {
                Move(_playerTransform.position.x - _transform.position.x, _playerTransform.position.y - _transform.position.y, Time.deltaTime);
            }
        }
    }
}
