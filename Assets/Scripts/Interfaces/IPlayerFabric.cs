using UnityEngine;


namespace Asteroids.Interfaces
{
    interface IPlayerFabric
    {
        IPlayer Create(GameObject gameObject);
    }
}
