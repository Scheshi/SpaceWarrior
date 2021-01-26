using UnityEngine;


namespace Asteroids.Interfaces
{
    interface IPlayerFabric
    {
        T Create<T>(GameObject gameObject) where T: IPlayer;
    }
}
