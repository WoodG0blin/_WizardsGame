using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    [CreateAssetMenu(fileName = "EnemiesConfig", menuName = "ScriptableObjects/EnemiesConfig", order = 2)]

    public class SO_EnemiesConfig : ScriptableObject
    {
        [SerializeField] private int _numberOfEnemies;

        [SerializeField] private float _speed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _hp;
        [SerializeField] private float _damage;
        [SerializeField] private float _force;

        [SerializeField] private View _stillEnemyPrefab;
        [SerializeField] private View _meleeEnemyPrefab;
        [SerializeField] private View _rangedEnemyPrefab;
        [SerializeField] private View _clonableEnemyPrefab;

        public int NumberOfEnemies{ get => _numberOfEnemies; }
        public float Speed { get => _speed; }
        public float Acceleration { get => _acceleration; }
        public float HP { get => _hp; }
        public float Damage { get => _damage; }
        public float Force { get => _force; }

        public View StillEnemyPrefab { get => _stillEnemyPrefab; }
        public View MeleeEnemyPrefab { get => _meleeEnemyPrefab; }
        public View RangedEnemyPrefab { get => _rangedEnemyPrefab; }
        public View ClonableEnemyPrefab { get => _clonableEnemyPrefab; }

    }
}
