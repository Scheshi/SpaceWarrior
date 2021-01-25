using UnityEngine;


namespace Asteroids.Interfaces
{
    public interface IPool
    {
        T Get<T>(Vector3 position, float points);

        void ReturnToPool(GameObject obj);

    }
}
