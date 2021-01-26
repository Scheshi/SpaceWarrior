using System.IO;
using Asteroids.Fabrics;
using Asteroids.Interfaces;
using Asteroids.Models;
using Models;
using UnityEngine;


namespace Asteroids.Services
{
    public sealed class EnemyParser
    {
        private readonly JsonSerializator _serializator = new JsonSerializator();
        private readonly string _path = Application.dataPath + "/Enemyes.json";

        public void Parse(params SerializableObjectInfo[] objects)
        {
            Debug.Log(_serializator.Serialize(objects));
        }

        
        public IEnemy[] Deparse()
        {
            if(!File.Exists(_path)) Debug.Log(_path);
            var resultEnemy = _serializator.Deserialize(_path);
            IEnemy[] enemyes = new IEnemy[resultEnemy.Length];
            var enemyFactory = new EnemyFactoryComposite();
            for (int i = 0; i < enemyes.Length; i++)
            {
                enemyes[i] = enemyFactory.Create
                    (new Health(resultEnemy[i].Unit.Health), resultEnemy[i].Unit.Type);
            }

            return enemyes;

        }
    }
}