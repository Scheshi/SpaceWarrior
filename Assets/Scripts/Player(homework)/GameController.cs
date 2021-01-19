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
            new PlayerInitializer(_playerData, _bulletData).Execute();
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


        //Сделал только добавление, ибо удаление нигде, по сути, и не нужно
        public static void AddUpdatable(IUpdatable updatable)
        {
            _updatables.Add(updatable);
        }
    }
}
