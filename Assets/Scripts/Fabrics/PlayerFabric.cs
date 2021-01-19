using Asteroid.Fabrics;
using Asteroid.Interfaces;
using UnityEngine;


namespace Asteroids
{
    internal sealed class PlayerFabric
    {
        private PlayerData _playerData;

        public PlayerFabric(PlayerData playerData)
        {
            _playerData = playerData;
        }


        public IPlayer Create(WeaponFabric weaponFabric, Health health)
        {
            var player = GameObject.Instantiate(_playerData.Prefab).transform;
            if(_playerData.Particles.TryGetComponent(out ParticleSystem _))
            {
                GameObject.Instantiate(_playerData.Particles, player);
            }
            var camera = Camera.main;
            camera.transform.parent = player;
            camera.transform.position = new Vector3(0.0f, 0.0f, _playerData.CameraOffset);

            var inputManager = new InputManager(camera, player);

            var moveTransform = new AccelerationMove(player, _playerData.Speed, _playerData.Acceleration);
            var rotation = new RotationShip(player);

            var ship = new ShipFabric(moveTransform, rotation).Create();

            inputManager.AccelerateDown += ship.RemoveAcceleration;
            inputManager.AccelerateUp += ship.AddAcceleration;
            inputManager.Move += ship.Move;
            inputManager.Rotation += ship.Rotation;

            var view = player.GetComponent<PlayerView>();
            health.Death += view.Dispose;
            view.Losses += health.Damage;


            var weapon = weaponFabric.Create(player.GetComponentInChildren<BarrelMarker>());

            inputManager.Fire += weapon.Fire;


            return view;
        }
    }
}
