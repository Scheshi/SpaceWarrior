using Asteroids.Interfaces;
using Asteroids.Models;
using Asteroids.Views;
using Models;
using UnityEngine;


namespace Asteroids.Fabrics
{
    class CometFactory : IEnemyFactory
    {
        public IEnemy Create(GameObject obj, Health health)
        {
            var enemy = GameObject.Instantiate(obj).AddComponent<Comet>();
            enemy.InjectHealth(health);
            return enemy;
        }

        public bool TryParse(SerializableObjectInfo serialized, out IEnemy enemy, GameController gameController, Vector3 position,
            Transform playerTransform)
        {
            if (serialized.Type.ToLower() == nameof(Comet).ToLower())
            {
                enemy = Create(new Health(serialized.Health), position, playerTransform, gameController);
                return true;
            }
            else
            {
                enemy = default;
                return false;
            }
        }

        public IEnemy Create(GameObject obj)
        {
            return GameObject.Instantiate(obj).AddComponent<Comet>();
        }

        public IEnemy Create(Health health, Vector3 position, Transform playerTransform, GameController gameController)
        {
            var enemy = GameObject.Instantiate(Resources.Load<Comet>("Prefabs/Comet"));
            enemy.InjectHealth(health);
            health.Death += enemy.Death;
            var cometTransform = enemy.transform;
            cometTransform.position = position;
            cometTransform.up = playerTransform.position - cometTransform.position;
            new CometMove(new MoveTransform(cometTransform, 1.0f), gameController)
                .Move(cometTransform.up.x, cometTransform.up.y, Time.deltaTime);
            return enemy;
        }
    }
}
