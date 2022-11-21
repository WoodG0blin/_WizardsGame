using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "ScriptableObjects/LevelConfig", order = 5)]

    public class SO_LevelConfig : ScriptableObject
    {
        [SerializeField] private SO_PlayerConfig _playerConfig;
        [SerializeField] private SO_EnemiesConfig _enemiesConfig;
        [SerializeField] private SO_WeaponsConfig _weaponsConfig;

        public SO_PlayerConfig PlayerConfig { get => _playerConfig; }
        public SO_EnemiesConfig EnemiesConfig { get => _enemiesConfig; }
        public SO_WeaponsConfig WeaponsConfig { get => _weaponsConfig; }

    }
}
