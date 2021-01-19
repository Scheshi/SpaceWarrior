using UnityEngine;


namespace Asteroid.Interfaces
{
    interface IEnemyFactory
    {
        IEnemy Create(GameObject obj);
    }
}
