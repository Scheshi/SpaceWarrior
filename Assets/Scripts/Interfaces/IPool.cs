using UnityEngine;


namespace Asteroids.Interfaces
{
    internal interface IPool
    {
        T Get<T>(Vector3 position, float points, Transform transform, GameController updaterController);

        void ReturnToPool(GameObject obj);

    }
}
