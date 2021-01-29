using Asteroids;
using Asteroids.Fabrics;
using Asteroids.Interfaces;
using Asteroids.Models;
using Asteroids.Views;
using UnityEngine;


namespace Services
{
    internal class PlayerInitializator
    {
        public (IPlayer, IShip) ConstructPlayer(PlayerData data)
        {
            var player = new PlayerFactory().Create<Player>(data.PlayerPrefab, data.ParticlesAroundPlayer, new Health(data.Hp));

            var playerTransform = player.transform;
            
            var mainCameraTransform = Camera.main.transform;
            mainCameraTransform.parent = playerTransform;
            mainCameraTransform.position = new Vector3(0.0f, 0.0f, data.CameraOffset);

            var moveTransform = new AccelerationMove(playerTransform, data.Speed, data.Acceleration);
            var rotation = new RotationShip(playerTransform);

            var ship = new ShipFabric(moveTransform, rotation).Create<Ship>();
            
            return (player, ship);
        }
    }
}