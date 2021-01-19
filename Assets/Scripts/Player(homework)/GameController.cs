using Asteroid.Fabrics;
using Asteroid.Interfaces;
using System.Collections.Generic;
using UnityEngine;


namespace Asteroids
{
    internal class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private BulletData _bulletData;
        private static List<IUpdatable> _updatables = new List<IUpdatable>();

        public void Start()
        {

            var player = (Player)new PlayerFabric().Create(_playerData.Prefab, _playerData.Particles, new Health(_playerData.Hp));

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




            var weapon = new WeaponFabric(_bulletData).Create(player.GetComponentInChildren<BarrelMarker>());

            inputManager.Fire += weapon.Fire;
        }

        private void Update()
        {
            for(int i = 0; i < _updatables.Count; i++)
            {
                _updatables[i].Update();
            }
        }

        internal static void RemoveUpdatable(IUpdatable updatable)
        {
            _updatables.Remove(updatable);
        }


        public static void AddUpdatable(IUpdatable updatable)
        {
            _updatables.Add(updatable);
        }
    }
}
