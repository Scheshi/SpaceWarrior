using Asteroid;
using Asteroid.Fabrics;
using Asteroid.Interfaces;
using Asteroid.ObjectPool;
using Asteroid.Views;
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

            var player = (Player)new PlayerFactory().Create(_playerData.Prefab, _playerData.Particles, new Health(_playerData.Hp));

            var playerTransform = player.transform;

            var inputManager = new InputManager(Camera.main, playerTransform);

            Camera.main.transform.parent = playerTransform;
            Camera.main.transform.position = new Vector3(0.0f, 0.0f, _playerData.CameraOffset);

            var moveTransform = new AccelerationMove(playerTransform, _playerData.Speed, _playerData.Acceleration);
            var rotation = new RotationShip(playerTransform);

            var ship = new ShipFabric(moveTransform, rotation).Create();

            inputManager.AccelerateDown += ship.RemoveAcceleration;
            inputManager.AccelerateUp += ship.AddAcceleration;
            inputManager.Move += ship.Move;
            inputManager.Rotation += ship.Rotation;

            var weapon = new WeaponFactory(_bulletData).Create(player.GetComponentInChildren<BarrelMarker>());

            inputManager.Fire += weapon.Fire;

            //Создание противника через фабрику
            //var enemy = new AsteroidFactory().Create(new Health(20.0f));

            var asteroid = EnemyObjectPool.GetEnemy<AsteroidEnemy>();

            //Создание противника статик методом(из фабрики)
            //Comet comet = (Comet)CometFactory.CreateEnemy(new Health(10.0f));
            
            var comet = EnemyObjectPool.GetEnemy<Comet>();
            
            comet.transform.position = new Vector2(
                player.transform.position.x + Random.Range(-5.0f, 5.0f),
                player.transform.position.y + Random.Range(-5.0f, 5.0f));

            comet.transform.up = playerTransform.position - comet.transform.position;
            new CometMove(new MoveTransform(comet.transform, 1.0f)).Move
                (comet.transform.up.x, comet.transform.up.y, Time.deltaTime);

            var enemy = EnemyObjectPool.GetEnemy<EnemyShip>();
            var persecutionMove = new UpdatablePersecutionMove(enemy.transform, playerTransform, _playerData.Speed);
            var persecutRotation = new UpdatablePersecutionRotation(enemy.transform, playerTransform);
            var enemyShip = new Ship(persecutionMove, persecutRotation);
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

        internal static void AddUpdatable(IUpdatable updatable)
        {
            if (updatable is IFrameUpdatable) _updatables.Add(updatable as IFrameUpdatable);
            if (updatable is IFixedUpdatable) _fixedUpdatables.Add(updatable as IFixedUpdatable);
        }


        public static void RemoveUpdatable(IUpdatable updatable)
        {
            if (updatable is IFrameUpdatable) _updatables.Remove(updatable as IFrameUpdatable);
            if (updatable is IFixedUpdatable) _fixedUpdatables.Remove(updatable as IFixedUpdatable);
        }
    }
}
