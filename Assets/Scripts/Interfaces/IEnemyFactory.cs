using Asteroids.Models;
using Models;
using UnityEngine;


namespace Asteroids.Interfaces
{
    public interface IEnemyFactory
    {
        bool TryParse(SerializableObjectInfo serialized, out IEnemy enemy, GameController gameController,
            Vector3 position, Transform playerTransform);

        IEnemy Create(Health health, Vector3 position, Transform playerTransform, GameController gameController);
    }
}
