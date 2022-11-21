using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    public class Sword : Weapon
    {
        public Sword(SO_WeaponConfig config, Transform parent) : base(config, parent) { }

        protected override void AttackActions(ITarget target)
        {
            target.DealDamage(_power);
        }
    }
}
