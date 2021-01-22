using Asteroids;
using UnityEngine;


internal class MoveForce : IMove
{
    private Rigidbody2D _rigidbody;

    public float Speed { get; protected set; }

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
