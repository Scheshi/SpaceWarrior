using Asteroids.Interfaces;
using Asteroids.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.Models
{
    public class MenuPanel :  IBaseUI
    {
        private GameObject _panel;
        public MenuPanel(GameObject panel)
        {
            var canvas = GameObject.FindObjectOfType<Canvas>();
            if (!canvas)
            {
                canvas = new GameObject("Canvas").AddComponent<CanvasScaler>()
                    .gameObject.AddComponent<GraphicRaycaster>()
                    .gameObject.AddComponent<Canvas>();
            }

            _panel = Object.Instantiate(panel, canvas.transform);
            Cancel();
        }

        public void Execute()
        {
            _panel.SetActive(true);
        }

        public void Cancel()
        {
            _panel.SetActive(false);
        }
    }
}