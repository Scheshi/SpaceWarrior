using Asteroids.Interfaces;
using System;
using System.Collections.Generic;
using Asteroids.Fabrics;
using Asteroids;
using System.Linq;
using Asteroids.Models;
using Asteroids.Services;
using UnityEngine;


namespace Asteroids.ObjectPool
{
    internal class EnemyObjectPool : IPool
    {
        private readonly Dictionary<string, HashSet<IEnemy>> _enemyCollection = new Dictionary<string, HashSet<IEnemy>>();

        private static IEnemy CreateEnemy(string typeString, float health)
        {
            IEnemy enemy = null;
            switch(typeString)
            {
                case "AsteroidEnemy":
                    enemy = new AsteroidFactory().Create(new Health(health));
                    break;
                case "Comet":
                    enemy = CometFactory.CreateEnemy(new Health(health));
                    break;
                case "EnemyShip":
                    enemy = new EnemyShipFactory().Create(new Health(health));
                    break;
                default:
                    throw new NullReferenceException("Такого типа нет в пуле объектов врагов");
            }
            return enemy;
        }


        private HashSet<IEnemy> GetListOfEnemy(string typeString)
        {
            return _enemyCollection.ContainsKey(typeString) ? _enemyCollection[typeString] : _enemyCollection[typeString] = new HashSet<IEnemy>();
        }

        public IEnemy Get<IEnemy>(Vector3 position, float points)
        {
            var type = typeof(IEnemy).Name;
            
            var enemy = GetListOfEnemy(type).FirstOrDefault(x => (x is MonoBehaviour) && !(x as MonoBehaviour).gameObject.activeSelf);


            if(enemy == null)
            {
                Debug.Log("Пустой враг. Создаю нового");
                enemy = CreateEnemy(type, points);
                _enemyCollection[type].Add(enemy);
            }
            enemy.InjectHealth(new Health(points));
            GameObject go = null;
            if (enemy.TryGetAbstract<MonoBehaviour>(out var enemyMono))
            {
                go = enemyMono.gameObject;
                go.transform.position = position;
                go.SetActive(true);
            }
            else
            {
                Debug.LogError($"Тип {typeof(IEnemy).Name} не является MonoBehaviour");
                
            }
            return (IEnemy)enemy;
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}
