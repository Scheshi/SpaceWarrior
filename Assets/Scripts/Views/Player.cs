using Asteroids.Interfaces;
using Asteroids;
using System;
using Asteroids.Models;
using UnityEngine;


namespace Asteroids.Views
{
    internal sealed class Player : MonoBehaviour, IDisposable, IPlayer
    {
        private Health _health;

        public Health Health => _health;

        public void Damage(float point)
        {
            _health.Damage(point);
        }

        public void Death()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        public void InjectHealth(Health health)
        {
            if (_health == null)
            {
                _health = health;
            }
        }
    }
}
