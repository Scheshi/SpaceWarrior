using System;


namespace Asteroids
{
    internal class Health
    {
        public event Action Death;
        public float Hp { get; private set; }

        public Health(float health)
        {
            Hp = health;
        }



        public void Damage(float point)
        {
            Hp -= point;
            if (Hp <= 0)
            {
                Death?.Invoke();
            }
        }
    }
}
