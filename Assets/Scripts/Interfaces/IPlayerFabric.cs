using UnityEngine;


namespace Asteroid.Interfaces
{
    interface IPlayerFabric
    {
        IPlayer Create(GameObject gameObject);
    }
}
