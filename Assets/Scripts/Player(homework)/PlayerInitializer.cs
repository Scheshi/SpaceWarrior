using UnityEngine;


namespace Asteroids
{
    public class PlayerInitializer
    {
        private PlayerData _playerData;
        private BulletData _bulletData;

        public PlayerInitializer(PlayerData playerData, BulletData bulletData)
        {
            _playerData = playerData;
            _bulletData = bulletData;
        }


        public void Execute()
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
            GameController.AddUpdatable(inputManager);

            var moveTransform = new AccelerationMove(player, _playerData.Speed, _playerData.Acceleration);
            var rotation = new RotationShip(player);

            new ShipInitializator(inputManager, moveTransform, rotation);

            var controller = new PlayerController(_playerData.Hp);
            var view = player.GetComponent<PlayerView>();
            controller.Death += view.Dispose;
            view.Collision += controller.Damage;


            var bullet = new BulletController(
                _bulletData.Bullet,
                player.GetComponentInChildren<BarrelMarker>().transform,
                _bulletData.Force);

            inputManager.Fire += bullet.Fire;

        }
    }
}
