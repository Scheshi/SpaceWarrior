using UnityEngine;


namespace Asteroids.Interfaces
{
    interface IEnemyFactory
    {
        IEnemy Create(GameObject obj);
    }
}
