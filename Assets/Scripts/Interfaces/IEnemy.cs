using System;
using Asteroids.Models;


namespace Asteroids.Interfaces
{
    public interface IEnemy : IDamagable
    {
        event Action<string> ScoreUp;
        
        float Attack { get; set; }
        float Defence { get; set; }
        
        void InjectHealth(Health health);
    }
}
