using Asteroids.Models;


namespace Asteroids.Interfaces
{
    public interface IDamagable
    {
        Health Health { get; }

        void Damage(float point);

        void Death();
    }
}
