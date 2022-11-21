using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

namespace Wizards
{
    public class LevelManager : IExecutor
    {
        private SO_LevelConfig _config;
        private InputController _input;
        private Player _player;
        private EnemiesController _enemyController;
        private Transform _poolRoot;
        private GameMenu _menu;

        private int _score;
        private float _playerHealth;
        private int _difficulty;
        private int _damagelevel;

        private List<IExecutable> _executables;
        public LevelManager(SO_LevelConfig config, Transform poolRoot, InputController input, GameMenu gameMenu)
        {
            _config = config;
            _poolRoot = poolRoot;
            _input = input;
            _menu = gameMenu;
        }

        public event Action<string> OnMessage;

        public void Start()
        {
            _executables = new List<IExecutable>();

            Services.SetService(new Pool<Bullet>(
                new SpellsBuilder(_config.WeaponsConfig.Spell)
                .SetScale(0.2f)
                .SetCollider2D(SpellsBuilder.ColliderType.circle, 0.5f)
                .SetRigidbody2D(1)
                .SetColor(new Color(0.8f, 0.28f, 0.28f, 1f)),
                _poolRoot, 10));
            Services.SetService(new WeaponFactory(_config.WeaponsConfig));

            _player = new Player(_config.PlayerConfig, Services.GetService<WeaponFactory>().CallForWeapon(Weapon.WeaponType.playerwand));
            _player.OnDeath += () => { Time.timeScale = 0; };
            _player.OnDeath += _menu.GameOver;
            _player.OnHealthChange += _menu.SetHealthValue;
            _player.OnHealthChange += UpdatePlayerHealth;
            _playerHealth = _player.Health;
            _menu.SetHealthValue(_player.Health);

            _input.OnInputsChange += _player.SetInputs;

            _enemyController = new EnemiesController(this, new EnemyFactory(_config.EnemiesConfig), _config.EnemiesConfig.NumberOfEnemies);
            _enemyController.Target = _player;
            _enemyController.OnEnemyKill += _menu.SetScoreValue;
            _enemyController.OnEnemyKill += UpdateScore;
            _score = 0;
            _menu.SetScoreValue(0);

            OnMessage += _menu.SetMessage;
        }

        public void Update()
        {
            foreach (IExecutable ex in _executables) ex.Execute();
        }

        public void End()
        {

        }

        public void AddExecutable(IExecutable executable)
        {
            _executables.Add(executable);
        }

        private void UpdateScore(int score)
        {
            _score = score;
            ManageLevel();
        }

        private void UpdatePlayerHealth(float health)
        {
            _playerHealth = health;
            ManageLevel();
        }

        private void ManageLevel()
        {
            if (_score > _difficulty * 10)
            {
                _difficulty++;
                _enemyController.MaxEnemies++;
            }

            if (_score == 5 && _damagelevel == 0)
            {
                _player.Weapon = Services.GetService<WeaponFactory>().ModifyWeapon(_player.Weapon, Modifier.Type.aim);
                SetMessage($"You killed {_score} enemies! You gained AIM to your weapon");
            }

            if (_score == 10 && _damagelevel == 0)
            {
                _player.Weapon = Services.GetService<WeaponFactory>().ModifyWeapon(_player.Weapon, Modifier.Type.boost);
                SetMessage($"You killed {_score} enemies! You gained BOOST to your weapon");
            }


            if (_playerHealth <= 50 && _damagelevel == 0)
            {
                _damagelevel++;
                if (_player.Weapon is ModifiedWeapon mw) _player.Weapon = Services.GetService<WeaponFactory>().RemoveModification(mw, Modifier.Type.boost);
                SetMessage($"You have critical damage ({_damagelevel}). You lost your BOOST");
            }

            if (_playerHealth <= 40 && _damagelevel == 1)
            {
                _damagelevel++;
                if (_player.Weapon is ModifiedWeapon mw) _player.Weapon = Services.GetService<WeaponFactory>().RemoveModification(mw, Modifier.Type.aim);
                SetMessage($"You have critical damage ({_damagelevel}). You lost your AIM");
            }

            if (_playerHealth <= 30 && _damagelevel == 2)
            {
                _damagelevel++;
                _player.Weapon = Services.GetService<WeaponFactory>().ModifyWeapon(_player.Weapon, Modifier.Type.fault);
                SetMessage($"You have critical damage ({_damagelevel}). Now your weapon is faulty");
            }
        }

        private void SetMessage(string message)
        {
            OnMessage?.Invoke(message);
            Services.GetService<Clock>().SetTimer(5, () => { OnMessage?.Invoke(""); });
        }
    }
}
