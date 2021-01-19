using System;


namespace Asteroids
{
    internal class PlayerController
    {
        public event Action Death;
        private float _hp;

        public PlayerController(float health)
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
