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
            IEnemy[] enemyes = new IEnemy[resultEnemy.Length];
            var enemyFactory = new EnemyFactoryComposite();
            
            for (int i = 0; i < enemyes.Length; i++)
            {
                enemyes[i] = enemyFactory.Create
                    (new Health(resultEnemy[i].Unit.Health),
                    resultEnemy[i].Unit.Type,
                    player,
                    controller,
                    //Затычка, задумываю, чтобы записывать в жсоне позицию для спавна
                    new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f))
                    );
            }

            return enemyes;

        }
    }
}