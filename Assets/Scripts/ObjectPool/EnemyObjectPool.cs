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
        private readonly EnemyFactoryComposite _enemyFactory = new EnemyFactoryComposite();

        public void InitializeEnemyesFromParser(EnemyParser parser)
        {
            var deparseEnemyes = parser.Deparse();
            foreach (var enemy in deparseEnemyes)
            {
                GetListOfEnemy(enemy.GetType().Name).Add(enemy);
            }
        }
        
        private HashSet<IEnemy> GetListOfEnemy(string typeString)
        {
            return _enemyCollection.ContainsKey(typeString)
                ? _enemyCollection[typeString]
                : _enemyCollection[typeString] = new HashSet<IEnemy>();
        }

        public TEnemy Get<TEnemy>(Vector3 position, float points)
        {
            var type = typeof(TEnemy).Name;

            var list = GetListOfEnemy(type);
            
            var enemy = list.FirstOrDefault(x => (x.TryGetAbstract<MonoBehaviour>(out var mono)) && !mono.gameObject.activeSelf);


            if(enemy == null)
            {
                Debug.Log("Пустой враг. Создаю нового");
                enemy = _enemyFactory.Create(new Health(points), type);
                list.Add(enemy);
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
                Debug.LogError($"Тип {typeof(TEnemy).Name} не является MonoBehaviour");
                
            }
            return (TEnemy)enemy;
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}
