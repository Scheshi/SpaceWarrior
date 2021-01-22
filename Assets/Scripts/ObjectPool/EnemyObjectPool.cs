using Asteroids.Interfaces;
using System;
using System.Collections.Generic;
using Asteroids.Fabrics;
using Asteroids;
using System.Linq;
using UnityEngine;


namespace Asteroids.ObjectPool
{
    internal class EnemyObjectPool : IPool
    {
        private readonly Dictionary<string, HashSet<IEnemy>> _enemyCollection = new Dictionary<string, HashSet<IEnemy>>();

        private static IEnemy CreateEnemy(string typeString)
        {
            IEnemy enemy = null;
            switch(typeString)
            {
                case "AsteroidEnemy":
                    enemy = new AsteroidFactory().Create(new Health(20.0f));
                    break;
                case "Comet":
                    enemy = CometFactory.CreateEnemy(new Health(20.0f));
                    break;
                case "EnemyShip":
                    enemy = new EnemyShipFactory().Create(new Health(30.0f));
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

        public IEnemy Get<IEnemy>()
        {
            var type = typeof(IEnemy).Name;

            var enemy = GetListOfEnemy(type).FirstOrDefault(x => !(x as MonoBehaviour).gameObject.activeSelf);

            if(enemy == null)
            {
                Debug.Log("Пустой враг. Создаю нового");
                enemy = CreateEnemy(type);
                _enemyCollection[type].Add(enemy);
            }

            (enemy as MonoBehaviour).gameObject.SetActive(true);
            return (IEnemy)enemy;
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}
