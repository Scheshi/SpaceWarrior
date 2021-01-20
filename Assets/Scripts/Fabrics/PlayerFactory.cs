using Asteroids.Interfaces;
using Asteroids;
using UnityEngine;

namespace Asteroids.Fabrics
{
    class PlayerFactory : IPlayerFabric
    {
        public IPlayer Create(GameObject gameObject, Health health)
        {
            var player = GameObject.Instantiate(gameObject).AddComponent<Player>();
            player.InjectHealth(health);
            health.Death += player.Dispose;

            return player;
        }

        public IPlayer Create(GameObject gameObject)
        {
            return GameObject.Instantiate(gameObject).AddComponent<Player>();
        }

        public IPlayer Create(GameObject gameObject, GameObject particles, Health health)
        {
            var player = (Player)Create(gameObject, health);
            GameObject.Instantiate(particles, player.transform);
            return player;
        }
    }
}
