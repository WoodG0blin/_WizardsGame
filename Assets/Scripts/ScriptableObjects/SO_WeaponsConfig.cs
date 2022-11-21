using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    [CreateAssetMenu(fileName = "WeaponsConfig", menuName = "ScriptableObjects/WeaponsConfig", order = 3)]

    public class SO_WeaponsConfig : ScriptableObject
    {
        [SerializeField] private SO_WeaponConfig[] _weapons;
        [SerializeField] private View _aim;
        [SerializeField] private Sprite _spell;

        public SO_WeaponConfig this[Weapon.WeaponType index]
        {
            get
            {
                for (int i = 0; i < _weapons.Length; i++)
                    if (_weapons[i].Type == index) return _weapons[i];
                return null;
            }
        }
        
        public View Aim { get => _aim; }
        public Sprite Spell { get => _spell; }
    }
}
