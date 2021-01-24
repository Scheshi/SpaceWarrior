using Asteroids.Models;


namespace Asteroids.Interfaces
{
    interface IDamagable
    {
        Health Health { get; }

        void Damage(float point);

        void Death();
    }
}
