using Asteroids;
using UnityEngine;


namespace Asteroids.Models
{
    internal class MoveForce : IMove
    {
        private readonly Rigidbody2D _rigidbody;

        public float Speed { get; private set; }

        public MoveForce(Rigidbody2D rigidbody, float speed)
        {
            Speed = speed;
            _rigidbody = rigidbody;
        }



        public void Move(float horizontal, float vertical, float deltaTime)
        {
            _rigidbody.MovePosition(new Vector3(horizontal, vertical, 0.0f) * Speed);
        }
    }
}
