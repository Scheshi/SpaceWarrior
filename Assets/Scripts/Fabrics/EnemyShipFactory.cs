using Asteroids.Interfaces;
using Asteroids.Views;
using Asteroids;
using Asteroids.Models;
using UnityEngine;


namespace Asteroids.Fabrics
{
    class EnemyShipFactory : IEnemyFactory
    {
        public IEnemy Create(GameObject obj)
        {
            return GameObject.Instantiate(obj).AddComponent<EnemyShip>();
        }

        public  IEnemy Create(Health health)
        {
            var enemy = GameObject.Instantiate(Resources.Load<EnemyShip>("Prefabs/Enemy"));
            enemy.InjectHealth(health);
            health.Death += enemy.Death;
            return enemy;
        }
    }
}
