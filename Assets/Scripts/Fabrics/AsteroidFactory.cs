﻿using Asteroid.Interfaces;
using Asteroid.Views;
using Asteroids;
using UnityEngine;


namespace Asteroid.Fabrics
{
    class AsteroidFactory : IEnemyFactory
    {
        public IEnemy Create(GameObject obj, Health health)
        {
            var enemy = GameObject.Instantiate(obj).AddComponent<AsteroidEnemy>();
            enemy.InjectHealth(health);
            return enemy;
        }

        public IEnemy Create(GameObject obj)
        {
            return GameObject.Instantiate(obj).AddComponent<AsteroidEnemy>();
        }

        public IEnemy Create(Health health)
        {
            var enemy = GameObject.Instantiate(Resources.Load<AsteroidEnemy>("Prefabs/Asteroid"));
            enemy.InjectHealth(health);
            return enemy;
        }
    }
}
