using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    public class ModifiedWeapon : IWeapon
    {
        protected Weapon _weapon;
        Dictionary<Modifier.Type, IModifier> _modifiers;
        public Action<IWeapon, ITarget> OnAttack;
        public View Construction { get => _weapon.Instance; }
        public ModifiedWeapon(Weapon weapon) { _weapon = weapon; _modifiers = new Dictionary<Modifier.Type, IModifier>(); }
        public bool Modified { get => _modifiers.Count > 0; }

        public Vector2 Position { get => _weapon.Position; }
        public Vector2 Direction { get => _weapon.Direction; }
        public bool Ready { get => _weapon.Ready; }
        public virtual void Attack(ITarget target)
        {
            if(OnAttack != null) OnAttack.Invoke(_weapon, target);
            else _weapon.Attack(target);
        }

        public void AddModifier(IModifier modifier)
        {
            if (modifier != null)
            {
                if (!_modifiers.ContainsKey(modifier.type))
                {
                    _modifiers.Add(modifier.type, modifier);
                    modifier.SetModifier(this);
                }
            }
        }

        public void RemoveModifier(Modifier.Type type)
        {
            if (_modifiers.ContainsKey(type))
            {
                _modifiers[type].RemoveModifier(this);
                _modifiers.Remove(type);
            }
        }

        public Weapon BaseWeapon { get => _weapon; }
    }
}
