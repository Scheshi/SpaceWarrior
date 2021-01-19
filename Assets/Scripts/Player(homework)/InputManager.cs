using System;
using UnityEngine;


namespace Asteroids
{
    public class InputManager : IUpdatable
    {
        public event Action<float, float, float> Move;
        public event Action<Vector3> Rotation;
        public event Action AccelerateUp;
        public event Action AccelerateDown;
        public event Action Fire;

        private Camera _camera;
        private Transform _inputerObject;

        private string _fireAxis = "Fire1";
        private string _horizontalAxis = "Horizontal";
        private string _verticalAxis = "Vertical";

        private string _accelerateAxis = "Fire2";
        
        public InputManager(Camera camera, Transform playerTransform)
        {
            _camera = camera;
            _inputerObject = playerTransform;
        }

        public void Update()
        {
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

    }
}
