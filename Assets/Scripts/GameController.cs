using Asteroids.Fabrics;
using Asteroids.Interfaces;
using Asteroids.ObjectPool;
using Asteroids.Services;
using Asteroids.Views;
using System.Collections.Generic;
using Asteroids.Models;
using Models;
using UnityEngine;


namespace Asteroids
{
    internal class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private BulletData _bulletData;
        private readonly List<IFrameUpdatable> _updatables = new List<IFrameUpdatable>();
        private readonly List<IFixedUpdatable> _fixedUpdatables = new List<IFixedUpdatable>();

        public void Start()
        {
            
            var enemyPool = new EnemyObjectPool();

            //Добавление пулла в локатор

            ServiceLocatorObjectPool.Send(enemyPool);
            ServiceLocatorObjectPool.Send(new BulletObjectPool());

            enemyPool.InitializeEnemyesFromParser(new EnemyParser());
            
            var player = new PlayerFactory().Create<Player>(_playerData.PlayerPrefab, _playerData.ParticlesAroundPlayer, new Health(_playerData.Hp));

            var playerTransform = player.transform;
            
            var inputManager = new InputManager(Camera.main, playerTransform, this);
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
                inputManager.Fire
                );

            inputManager.Fire += weapon.Fire;
            
            // и попытка его достать оттуда
            var asteroid = ServiceLocatorObjectPool.Get<EnemyObjectPool>().Get<AsteroidEnemy>(
                new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), 0.0f),
                10.0f
            );

            var comet = enemyPool.Get<Comet>(
                new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), 0.0f),
                15.0f
                );

            var cometTransform = comet.transform;

            //cometTransform.up = playerTransform.position - comet.transform.position;
            new CometMove(new MoveTransform(comet.transform, 1.0f), this)
                .Move(cometTransform.up.x, cometTransform.up.y, Time.deltaTime);

            var enemy = enemyPool.Get<EnemyShip>(
                new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), 0.0f),
                20.0f
                );

            var enemyShipTransform = enemy.transform;
            var persecutionMove = new UpdatablePersecutionMove(enemyShipTransform, playerTransform, _playerData.Speed / 2, this);
            var persecutionRotation = new UpdatablePersecutionRotation(enemyShipTransform, playerTransform, this);
            var enemyShip = new Ship(persecutionMove, persecutionRotation);
            enemy.InjectMovement(persecutionMove);

            var enemyWeapon = new WeaponFactory(
                    //Временный костыль. Потом придумаю, как реализовать это через инспектор
                new BulletData()
                {
                    Bullet = _bulletData.Bullet,
                    Damage = _bulletData.Damage / 2,
                    Force = _bulletData.Force / 2
                })
                .Create(
                enemy.GetComponentInChildren<BarrelMarker>(), 
                persecutionMove.Stoping
                );
            
            persecutionMove.Stoping += enemyWeapon.Fire;
        }

        private void Update()
        {
            for(int i = 0; i < _updatables.Count; i++)
            {
                _updatables[i].Update();
            }
        }

        private void FixedUpdate()
        {
            for(int i = 0; i < _fixedUpdatables.Count; i++)
            {
                _fixedUpdatables[i].FixedUpdate();
            }
        }

        public void AddUpdatable(IUpdatable updatable)
        {
            if (updatable is IFrameUpdatable) _updatables.Add(updatable as IFrameUpdatable);
            if (updatable is IFixedUpdatable) _fixedUpdatables.Add(updatable as IFixedUpdatable);
        }


        public void RemoveUpdatable(IUpdatable updatable)
        {
            if (updatable is IFrameUpdatable) _updatables.Remove(updatable as IFrameUpdatable);
            if (updatable is IFixedUpdatable) _fixedUpdatables.Remove(updatable as IFixedUpdatable);
        }
    }
}
