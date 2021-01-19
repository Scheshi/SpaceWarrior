using Asteroid.Interfaces;
using Asteroids;
using UnityEngine;


namespace Asteroid.Views
{
    internal sealed class AsteroidEnemy : MonoBehaviour, IEnemy
    {
        private Health _health;


        public void Damage(float point)
        {
            _health.Damage(point);
        }

        public void InjectHealth(Health health)
        {
            if(_health == null)
            {
                _health = health;
            }
        }
    }
}
