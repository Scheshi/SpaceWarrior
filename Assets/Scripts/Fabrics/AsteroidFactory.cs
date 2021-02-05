using Asteroids.Interfaces;
using Asteroids.Views;
using Asteroids;
using Asteroids.Models;
using UnityEngine;


namespace Asteroids.Fabrics
{
    [RequireComponent(typeof(CircleCollider2D))]
    class AsteroidFactory : IEnemyFactory
    {
        public IEnemy Create(GameObject obj, Health health)
        {
            var enemy = GameObject.Instantiate(obj).AddComponent<Asteroid>();
            enemy.InjectHealth(health);
            return enemy;
        }

        public IEnemy Create(GameObject obj)
        {
            return GameObject.Instantiate(obj).AddComponent<Asteroid>();
        }

        public IEnemy Create(Health health)
        {
            var enemy = GameObject.Instantiate(Resources.Load<Asteroid>("Prefabs/Asteroid"));
            enemy.InjectHealth(health);
            health.Death += enemy.Death;
            return enemy;
        }
    }
}
