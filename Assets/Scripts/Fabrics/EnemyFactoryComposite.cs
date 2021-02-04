using System;
using Asteroids.Interfaces;
using Asteroids.Models;
using Asteroids.Services;
using Asteroids.Views;
using Models;
using UnityEngine;


namespace Asteroids.Fabrics
{
    internal sealed class EnemyFactoryComposite
    {
        private IEnemyFactory[] _factories;
        
        public EnemyFactoryComposite()
        {
            _factories = new IEnemyFactory[]
            {
                new AsteroidFactory(),
                new CometFactory(),
                new EnemyShipFactory()
            };
        }
        
        
        
        public TEnemy Create<TEnemy>(Health health, Transform playerTransform, GameController gameController, Vector3 position)
        {
            return (TEnemy)Create(health, typeof(TEnemy).Name, playerTransform, gameController, position);
        }

        public IEnemy[] Parsing(SerializableObjectUnit[] objectInfos, Transform playerTransform, 
            GameController gameController, Vector2[] positions)
        {
            var enemies = new IEnemy[objectInfos.Length];
            for (int i = 0; i < objectInfos.Length; i++)
            {
                foreach (var item in _factories)
                {
                    if (item.TryParse(objectInfos[i].Unit, out var enemy, gameController, positions[i], playerTransform))
                    {
                        enemies[i] = enemy;
                        break;
                    }
                }
                if (enemies[i] == null) throw new ArgumentException("Указан неверный тип " + objectInfos[i].Unit.Type);
            }
            return enemies;
        }
        
        
        public IEnemy Create(Health health, string typeName, Transform playerTransform, 
            GameController gameController, Vector3 position)
        {
            IEnemy enemy;
            switch (typeName.ToLower())
            {
                case "asteroid":
                    enemy = new AsteroidFactory().Create(health, position, playerTransform, gameController);;
                    break;
                case "comet":
                    enemy = new CometFactory().Create(health, position, playerTransform, gameController);
                    break;
                case "enemyship":
                    enemy = new EnemyShipFactory().Create(health, position, playerTransform, gameController);;
                    break;
                
                default:
                    throw new NullReferenceException("Нет такого типа в композиции фабрик");
            }
            return enemy;
        }
    }
}