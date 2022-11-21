using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "ScriptableObjects/WeaponConfig", order = 4)]
    public class SO_WeaponConfig : ScriptableObject
    {
        [SerializeField] private Weapon.WeaponType _type;

        [SerializeField] private float _damage;
        [SerializeField] private float _force;
        [SerializeField] private float _rangeAngle;
        [SerializeField] private float _rangeDistance;
        [SerializeField] private float _coolDown;

        [SerializeField] private View _prefab;

        public Weapon.WeaponType Type { get => _type; }
        public float Damage { get => _damage; }
        public float Force { get => _force; }
        public float RangeAngle { get => _rangeAngle; }
        public float RangeDistance { get => _rangeDistance; }
        public float CoolDown { get => _coolDown; }
        public View Prefab { get => _prefab; }
    }
}
