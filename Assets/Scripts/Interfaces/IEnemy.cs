namespace Asteroids.Interfaces
{
    interface IEnemy : IDamagable
    {
        void InjectHealth(Health health);
    }
}
