using Asteroids.Fabrics;
using Asteroids.Interfaces;
using Asteroids.Models;
using Asteroids.ObjectPool;
using Asteroids.Views;
using UnityEngine;


namespace Asteroids.Services
{
    internal class Game
    {
        private readonly PlayerData _playerData;
        private readonly BulletData _bulletData;
        private readonly GameController _gameController;
        
        public Game(PlayerData playerData, BulletData bulletData, GameController controller)
        {
            _playerData = playerData;
            _bulletData = bulletData;
            _gameController = controller;
        }
        
        public void Construct()
        {
            ServiceLocatorObjectPool.Send(new BulletObjectPool());
            var player = CreatePlayer();
            var enemyes = CreateEnemyes(((MonoBehaviour)player).transform);
        }


        private IEnemy[] CreateEnemyes(Transform playerTransform)
        {
            var enemyPool = new EnemyObjectPool();

            //Добавление пулла в локатор

            ServiceLocatorObjectPool.Send(enemyPool);
            
            return enemyPool.InitializeEnemyesFromParser(new EnemyParser(), playerTransform, _gameController);
        }

        private IPlayer CreatePlayer()
        {
            var player = new PlayerFactory().Create<Player>(_playerData.PlayerPrefab, _playerData.ParticlesAroundPlayer, new Health(_playerData.Hp));

            var playerTransform = player.transform;
            
            var inputManager = new InputManager(Camera.main, playerTransform, _gameController);
            var mainCameraTransform = Camera.main.transform;
            mainCameraTransform.parent = playerTransform;
            mainCameraTransform.position = new Vector3(0.0f, 0.0f, _playerData.CameraOffset);

            var moveTransform = new AccelerationMove(playerTransform, _playerData.Speed, _playerData.Acceleration);
            var rotation = new RotationShip(playerTransform);

            var ship = new ShipFabric(moveTransform, rotation).Create<Ship>();


            inputManager.AccelerateDown += ship.RemoveAcceleration;
            inputManager.AccelerateUp += ship.AddAcceleration;
            inputManager.Move += ship.Move;
            inputManager.Rotation += ship.Rotation;

            var weapon = new WeaponFactory(_bulletData)
                .Create(
                    player.GetComponentInChildren<BarrelMarker>(),
                    inputManager.Fire,
                    _playerData.WeaponData
                );

            inputManager.Fire += weapon.Fire;

            return player;
        }
    }
}