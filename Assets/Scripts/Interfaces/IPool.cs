using UnityEngine;


namespace Asteroids.Interfaces
{
    public interface IPool
    {
        T Get<T>();

        void ReturnToPool(GameObject obj);

    }
}
