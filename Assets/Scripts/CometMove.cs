using Asteroid.Interfaces;
using Asteroids;
using UnityEngine;


internal sealed class CometMove : IMove, IFrameUpdatable
{
    private IMove _move;

    private float _horizontal;
    private float _vertical;
    private float _deltaTime;

    public CometMove(IMove move)
    {
        _move = move;
        GameController.AddUpdatable(this);
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
}
