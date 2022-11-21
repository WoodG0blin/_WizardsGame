using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    public class Boost : Modifier
    {
        protected int _repetitions;
        protected int count;
        protected Clock _clock;
        public Boost(int repetitions) { _repetitions = repetitions; count = repetitions; _clock = Services.GetService<Clock>(); _type = Type.boost; }

        public override void SetModifier(ModifiedWeapon weapon)
        {
            weapon.OnAttack += BoostedAttack;
        }

        public override void RemoveModifier(ModifiedWeapon weapon)
        {
            weapon.OnAttack -= BoostedAttack;
        }

        protected void BoostedAttack(IWeapon weapon, ITarget target)
        {
            count--;
            weapon.Attack(target);
            if (count > 0) _clock.SetTimer(0.2f, () => { BoostedAttack(weapon, target); });
            else count = _repetitions;
        }
    }
}
