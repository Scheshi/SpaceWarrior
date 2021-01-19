using Asteroid.Fabrics;
using Asteroid.Interfaces;
using System;
using System.Collections;
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
            IPlayer player = new PlayerFabric(_playerData)
                .Create(
                new WeaponFabric(_bulletData),
                new Health(_playerData.Hp)
                );

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
