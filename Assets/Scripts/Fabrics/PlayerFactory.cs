using Asteroids.Interfaces;
using Asteroids;
using Asteroids.Models;
using Asteroids.Views;
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

        public TPlayer Create<TPlayer>(GameObject gameObject) where TPlayer: IPlayer
        {
            var player = (IPlayer)GameObject.Instantiate(gameObject).AddComponent<Player>();
            return (TPlayer)player;
        }

        public T Create<T>(GameObject gameObject, GameObject particles, Health health) where T: IPlayer
        {
            var player = Create(gameObject, health);
            GameObject.Instantiate(particles, ((MonoBehaviour)player).transform);
            return (T)player;
        }
    }
}
