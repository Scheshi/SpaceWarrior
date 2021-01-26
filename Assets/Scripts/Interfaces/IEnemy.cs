using Asteroids.Models;


namespace Asteroids.Interfaces
{
    public interface IEnemy : IDamagable
    {
        void InjectHealth(Health health);
    }
}
