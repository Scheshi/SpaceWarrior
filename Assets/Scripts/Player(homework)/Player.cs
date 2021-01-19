using Asteroid.Interfaces;
using System;


namespace Asteroids
{
    internal class Player : IPlayer
    {
        public event Action Death;
        private float _hp;

        public Player(float health)
        {
            _hp = health;
        }

        public void Damage()
        {
            if (_hp <= 0)
            {
                Death?.Invoke();
            }
            else
            {
                _hp--;
            }
        }
    }
}
