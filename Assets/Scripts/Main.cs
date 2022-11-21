using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Wizards
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private SO_LevelConfig _levelConfig;
        [SerializeField] private GameMenuView _gameMenuView;
        [SerializeField] private Transform _poolRoot;

        private InputController _input;
        private LevelManager _levelManager;
        private Clock _clock;

        void Start()
        {
            _clock = new Clock();
            Services.SetService(_clock);
            _input = new InputController();
            _levelManager = new LevelManager(_levelConfig, _poolRoot, _input, new GameMenu(_gameMenuView));
            _levelManager.Start();
        }

        void Update()
        {
            _clock.Tick(Time.deltaTime);
            _input.Update();
            _levelManager.Update();
        }
    }
}
