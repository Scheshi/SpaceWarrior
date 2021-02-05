using Asteroids.Interfaces;
using System;
using UnityEngine;


namespace Asteroids
{
    internal class InputManager : IFrameUpdatable, IDisposable
    {
        public event Action<float, float, float> Move;
        public event Action<Vector3> Rotation;
        public event Action AccelerateUp;
        public event Action AccelerateDown;
        public Action Fire;
        public Action ChangeToPrimary;
        public Action ChangeToSecond;
        
        private Camera _camera;
        private Transform _inputerObject;
        private GameController _game;

        private string _fireAxis = "Fire1";
        private string _horizontalAxis = "Horizontal";
        private string _verticalAxis = "Vertical";

        private string _accelerateAxis = "Fire2";

        private KeyCode _primaryWeapon = KeyCode.Alpha1;
        private KeyCode _secondWeapon = KeyCode.Alpha2;
        
        public InputManager(Camera camera, Transform playerTransform, GameController gameController)
        {
            _camera = camera;
            _inputerObject = playerTransform;
            _game = gameController;
            _game.AddUpdatable(this);
        }

        public void Update()
        {
            ChangeWeapon();
            FireInvoke();
            AccelerateInvoke();

            Move?.Invoke(
                    Input.GetAxis(_horizontalAxis),
                    Input.GetAxis(_verticalAxis),
                    Time.deltaTime
                );

            Rotation?.Invoke(Input.mousePosition - _camera.WorldToScreenPoint(_inputerObject.position));
        }

        private void FireInvoke()
        {
            if (Input.GetButtonDown(_fireAxis))
            {
                Fire?.Invoke();
            }
        }

        private void AccelerateInvoke()
        {
            if (Input.GetButtonDown(_accelerateAxis))
            {
                AccelerateUp?.Invoke();
            }
            else if (Input.GetButtonUp(_accelerateAxis))
            {
                AccelerateDown?.Invoke();
            }
        }

        public void Dispose()
        {
            _game.RemoveUpdatable(this);
        }

        public void ChangeWeapon()
        {
            if (Input.GetKeyDown(_primaryWeapon))
            {
                ChangeToPrimary?.Invoke();
            }

            else if(Input.GetKeyDown(_secondWeapon))

            {
                ChangeToSecond?.Invoke();
            }
        }
    }
}
