using Asteroid.Interfaces;
using Asteroid.Views;
using Asteroids;
using UnityEngine;


namespace Asteroid.Fabrics
{
    class CometFactory : IEnemyFactory
    {
        public IEnemy Create(GameObject obj, Health health)
        {
            var enemy = GameObject.Instantiate(obj).AddComponent<Comet>();
            enemy.InjectHealth(health);
            return enemy;
        }

        public IEnemy Create(GameObject obj)
        {
            return GameObject.Instantiate(obj).AddComponent<Comet>();
        }

        public IEnemy Create(Health health)
        {
            var enemy = GameObject.Instantiate(Resources.Load<Comet>("Prefabs/Comet"));
            enemy.InjectHealth(health);
            health.Death += enemy.Dispose;
            return enemy;
        }

        public static IEnemy CreateEnemy(Health health)
        {
            var enemy = new CometFactory().Create(health);
            return enemy;
        }
    }
}
