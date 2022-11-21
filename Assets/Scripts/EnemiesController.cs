using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace Wizards
{
    internal sealed class EnemiesController : Controller
    {
        private int _maxEnemies;
        private List<IEnemy> _enemies;
        private EnemyFactory _enemyFactory;
        private int _count = 0;

        private Clock _clock;
        internal ITarget Target { private get; set; }
        public event Action<int> OnEnemyKill;

        internal EnemiesController(IExecutor executor, EnemyFactory enemyFactory, int maxEnemies) : base(executor)
        {
            _maxEnemies = maxEnemies;
            _enemyFactory = enemyFactory;

            _enemies = new List<IEnemy>();
            
            _clock = Services.GetService<Clock>();
            _clock.SetTimer(Random.Range(3f, 5f), () => { DelayedAddition(); });
        }

        public int MaxEnemies { get => _maxEnemies; set => _maxEnemies = Math.Clamp(value, _maxEnemies, value); }

        public override void Execute()
        {
            if (_enemies != null)
            {
                foreach (IEnemy enemy in _enemies)
                {
                    enemy.MoveTo(Target.Position);
                    enemy.Attack(Target);
                }
            }
        }

        private void AddEnemy(EnemyFactory.EnemyType type)
        {
            if(_enemies.Count < _maxEnemies)
            {
                _enemies.Add(_enemyFactory.CreateEnemy(type));
                _enemies[_enemies.Count-1].OnDeath += RemoveEnemy;
            }
        }

        private void RemoveEnemy(IEnemy enemy)
        {
            _count++;
            OnEnemyKill?.Invoke(_count);
            _enemies.Remove(enemy);
            if(_enemies.Count < _maxEnemies) _clock.SetTimer(Random.Range(3f, 5f), () => { DelayedAddition(); });
        }

        void DelayedAddition()
        {
            AddEnemy(EnemyFactory.EnemyType.Random);
            if (_enemies.Count < _maxEnemies) _clock.SetTimer(Random.Range(3f, 5f), () => { DelayedAddition(); });
        }
    }
}
