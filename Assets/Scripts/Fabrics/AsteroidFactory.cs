using Asteroids.Interfaces;
using Asteroids.Views;
using Asteroids;
using Asteroids.Models;
using Models;
using UnityEditor;
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

        public bool TryParse(SerializableObjectInfo unit, out IEnemy enemy, GameController gameController,
            Vector3 position, Transform playerTransform)
        {
            if (unit.Type.ToLower() == nameof(Asteroid).ToLower())
            {
                enemy = Create(new Health(unit.Health), position, playerTransform, gameController);
                return true;
            }
            else
            {
                enemy = default;
                return false;
            }
        }

        public IEnemy Create(Health health, Vector3 position, Transform playerTransform, GameController gameController)
        {
            var enemy = GameObject.Instantiate(Resources.Load<Asteroid>("Prefabs/Asteroid"));
            enemy.InjectHealth(health);
            health.Death += enemy.Death;
            enemy.transform.position = position;
            return enemy;
        }

        public IEnemy Create(GameObject obj)
        {
            return GameObject.Instantiate(obj).AddComponent<Asteroid>();
        }
        
    }
}
