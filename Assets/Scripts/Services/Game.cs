using System;
using Assets.Scripts.Services;
using Asteroids.Fabrics;
using Asteroids.Interfaces;
using Asteroids.Models;
using Asteroids.ObjectPool;
using Services;
using UnityEngine;
using UnityEngine.UI;


namespace Asteroids.Services
{
    internal class Game
    {
        private readonly PlayerData _playerData;
        private readonly BulletData _bulletData;
        private readonly GameController _gameController;
        private InputManager _inputManager;
        
        private (IPlayer, IShip) _playerInfo;

        public IPlayer Player => _playerInfo.Item1;
        
        public IShip PlayerShip => _playerInfo.Item2;

        public InputManager Manager => _inputManager;

        
        
        public Game(PlayerData playerData, BulletData bulletData, GameController controller)
        {
            _playerData = playerData;
            _bulletData = bulletData;
            _gameController = controller;
        }


        public void Construct()
        {
            ServiceLocatorObjectPool.Send(new BulletObjectPool());
            _playerInfo = CreatePlayer(new WeaponFactory(_bulletData), _playerData.WeaponData);
            var enemies = CreateEnemies(((MonoBehaviour)Player).transform);

            var canvas = GameObject.FindObjectOfType<Canvas>();
            var text = new GameObject("scoreText").AddComponent<Text>();
            text.transform.parent = canvas.transform;
            var interpreter = new ScoreInterpreter(text);
            AudioClip forceClip = Resources.Load<AudioClip>("Audios/force_weapon");
            foreach (var enemy in enemies)
            {
                enemy.ScoreUp += interpreter.Interpret;
            }
            if (forceClip != null)
            {
                //Decorator
                var forceModification = new ForceModification(forceClip,
                    1.0f, 30.0f);
                forceModification.AddModification(PlayerShip.Weapon);
                //Decorator remove
                //forceModification.RemoveMofication(PlayerShip.Weapon);
            }
            else throw new NullReferenceException("Нет файла Resources/Audios/force_weapon");

        }


        private IEnemy[] CreateEnemies(Transform playerTransform)
        {
            var enemyPool = new EnemyObjectPool();

            //Добавление пулла в локатор

            ServiceLocatorObjectPool.Send(enemyPool);
            
            return enemyPool.InitializeEnemyesFromParser(new EnemyParser(), playerTransform, _gameController);
        }

        private (IPlayer, IShip) CreatePlayer(IWeaponFabric weaponFabric, WeaponData data)
        { 
            var playerInfo = new PlayerInitializator().ConstructPlayer(_playerData);

            if (playerInfo.Item1.TryGetAbstract<MonoBehaviour>(out var monoPlayer))
            {
                _inputManager = new InputManager(Camera.main, monoPlayer.transform,
                    _gameController);
                IWeapon weapon = weaponFabric.Create(monoPlayer.GetComponentInChildren<BarrelMarker>(),
                    _inputManager.Fire,
                    data);
                //Weapon Locker
                var weaponProxy = new WeaponLocker(weapon);
                _inputManager.Fire += weaponProxy.Fire;

                var ship = playerInfo.Item2;
                ship.SetNewWeapon(weapon);
                _inputManager.Move += ship.Move;
                _inputManager.Rotation += ship.Rotation;
                return playerInfo;
            }
            else throw new NullReferenceException("Тип " + playerInfo.Item1.GetType() + " не является MonoBehaviour");


        }
    }
}