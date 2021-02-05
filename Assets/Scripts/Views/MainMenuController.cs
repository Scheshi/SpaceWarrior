using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Interfaces;
using Asteroids.Models;
using Asteroids.Services;
using UnityEngine;


namespace Asteroids.Views
{
    public class MainMenuController : IFrameUpdatable
    {
        public event Action StartGame;
        Dictionary<StateUI, MenuPanel> _panels;
        private IBaseUI _currentWindow;
        private Stack<StateUI> _uiStack = new Stack<StateUI>();
        private bool _isStartGame = false;

        public MainMenuController(Dictionary<StateUI, MenuPanel> menuDictionary)
        {
            _panels = menuDictionary;
        }
        
        public void Execute(StateUI ui, bool isSave = true)
        {
            Time.timeScale = 0.0f;
            if (_currentWindow != null)
            {
                _currentWindow.Cancel();
            }

            _currentWindow = _panels.FirstOrDefault(x => x.Key == ui).Value;
            
            _currentWindow.Execute();
            if (isSave)
            {
                _uiStack.Push(ui);
            }
        }

        public void Close()
        {
            
            _currentWindow.Cancel();
            StartGame?.Invoke();
            _isStartGame = true;
            Time.timeScale = 1.0f;
        }
        public void Update()
            {
             if(!_isStartGame)
             {
                 if (Input.GetKeyDown(KeyCode.P))
                 {
                     Close();
                 }

                 if (Input.GetKeyDown(KeyCode.S))
                 {
                     Execute(StateUI.Setting);
                 }

                 if (Input.GetKeyDown(KeyCode.M))
                 {
                     Execute(StateUI.Menu);
                 }

                 if (Input.GetKeyDown(KeyCode.Escape))
                 {
                     Execute(_uiStack.Pop());
                 }
             }
            }
    }
}