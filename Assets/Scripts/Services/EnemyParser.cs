using System.IO;
using System.Linq;
using Asteroids.Fabrics;
using Asteroids.Interfaces;
using Asteroids.Models;
using Models;
using UnityEngine;


namespace Asteroids.Services
{
    internal sealed class EnemyParser
    {
        private readonly JsonSerializator _serializator = new JsonSerializator();
        private readonly string _path = Application.dataPath + "/Enemyes.json";

        public void Parse(params SerializableObjectInfo[] objects)
        {
            Debug.Log(_serializator.Serialize(objects));
        }

        
        public IEnemy[] Deparse(Transform player, GameController controller)
        {
            if(!File.Exists(_path)) Debug.Log(_path);
            var resultEnemy = _serializator.Deserialize(_path);
            var enemyFactory = new EnemyFactoryComposite();
            var positions = new Vector2[resultEnemy.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
            }
            
            return enemyFactory.Parsing(resultEnemy, player, controller, positions);

        }
    }
}