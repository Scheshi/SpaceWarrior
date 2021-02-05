using System;
using Asteroids.Models;


namespace Asteroids.Interfaces
{
    public interface IEnemy : IDamagable
    {
        float Attack { get; set; }
        float Defence { get; set; }
        
        void InjectHealth(Health health);
    }
}
