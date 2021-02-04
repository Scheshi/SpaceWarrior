using Asteroids.Interfaces;
using Asteroids.Services;
using System.Collections.Generic;
using UnityEngine;


namespace Asteroids
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private BulletData _bulletData;
        private Game _game;
        private readonly List<IFrameUpdatable> _updatables = new List<IFrameUpdatable>();
        private readonly List<IFixedUpdatable> _fixedUpdatables = new List<IFixedUpdatable>();

        public void Start()
        {
            //Facade
            _game = new Game(_playerData, _bulletData, this);
            _game.Construct();
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
