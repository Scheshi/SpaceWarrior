using Asteroids.Interfaces;
using System;
using System.Collections.Generic;
using Asteroids.Fabrics;
using Asteroids;
using System.Linq;
using UnityEngine;


namespace Asteroids.ObjectPool
{
    internal static class EnemyObjectPool
    {
        private static readonly Dictionary<string, HashSet<IEnemy>> _enemyCollection = new Dictionary<string, HashSet<IEnemy>>();

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


        private static HashSet<IEnemy> GetListOfEnemy(string typeString)
        {
            return _enemyCollection.ContainsKey(typeString) ? _enemyCollection[typeString] : _enemyCollection[typeString] = new HashSet<IEnemy>();
        }

        public static T GetEnemy<T>() where T: IEnemy
        {
            var type = typeof(T).Name;

            var enemy = GetListOfEnemy(type).FirstOrDefault(x => !(x as MonoBehaviour).gameObject.activeSelf);

            if(enemy == null)
            {
                Debug.Log("Пустой враг. Создаю нового");
                enemy = CreateEnemy(type);
                _enemyCollection[type].Add(enemy);
            }

            (enemy as MonoBehaviour).gameObject.SetActive(true);
            return (T)enemy;
        }

        public static void ReturnToPool(IEnemy enemy)
        {
            var go = (enemy as MonoBehaviour).gameObject;
            go.SetActive(false);
        }
    }
}
