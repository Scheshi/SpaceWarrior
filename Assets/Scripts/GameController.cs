﻿using Asteroids.Fabrics;
using Asteroids.Interfaces;
using Asteroids.ObjectPool;
using Asteroids.Services;
using Asteroids.Views;
using System.Collections.Generic;
using UnityEngine;


namespace Asteroids
{
    internal class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private BulletData _bulletData;
        private readonly static List<IFrameUpdatable> _updatables = new List<IFrameUpdatable>();
        private readonly static List<IFixedUpdatable> _fixedUpdatables = new List<IFixedUpdatable>();

        public void Start()
        {

            var player = (Player)new PlayerFactory().Create(_playerData.PlayerPrefab, _playerData.ParticlesAroundPlayer, new Health(_playerData.Hp));

            var playerTransform = player.transform;

            var inputManager = new InputManager(Camera.main, playerTransform, this);

            Camera.main.transform.parent = playerTransform;
            Camera.main.transform.position = new Vector3(0.0f, 0.0f, _playerData.CameraOffset);

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
                ref inputManager.Fire
                );

            var enemyPool = new EnemyObjectPool();

            //Добавление пулла в локатор и попытка его достать оттуда

            ServiceLocatorObjectPool.Send(enemyPool);
            ServiceLocatorObjectPool.Send(new BulletObjectPool());

            var asteroid = ServiceLocatorObjectPool.Get<EnemyObjectPool>().Get<AsteroidEnemy>(
                new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), 0.0f),
                10.0f
                );

            //var asteroid = enemyPool.Get<AsteroidEnemy>();

            var comet = enemyPool.Get<Comet>(
                new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), 0.0f),
                15.0f
                );
            
            comet.transform.position = new Vector2(
                player.transform.position.x + Random.Range(-5.0f, 5.0f),
                player.transform.position.y + Random.Range(-5.0f, 5.0f));

            comet.transform.up = playerTransform.position - comet.transform.position;
            new CometMove(new MoveTransform(comet.transform, 1.0f), this)
                .Move(comet.transform.up.x, comet.transform.up.y, Time.deltaTime);

            var enemy = enemyPool.Get<EnemyShip>(
                new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), 0.0f),
                20.0f
                );
            var persecutionMove = new UpdatablePersecutionMove(enemy.transform, playerTransform, _playerData.Speed / 2, this);
            var persectionRotation = new UpdatablePersecutionRotation(enemy.transform, playerTransform, this);
            var enemyShip = new Ship(persecutionMove, persectionRotation);
            enemy.InjectMovement(persecutionMove);

            var enemyWeapon = new WeaponFactory(
                new BulletData()
                {
                    Bullet = _bulletData.Bullet,
                    Damage = _bulletData.Damage / 2,
                    Force = _bulletData.Force / 2
                })
                .Create(
                enemy.GetComponentInChildren<BarrelMarker>(),
                ref persecutionMove.Stoping
                );
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
