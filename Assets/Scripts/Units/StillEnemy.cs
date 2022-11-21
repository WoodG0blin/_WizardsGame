using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    internal class StillEnemy : Enemy
    {
        public StillEnemy(View view, IWeaponPrototype weaponPrototype, float hp) : base(view, weaponPrototype, hp) { }

        public override void MoveTo(Vector2 destination)
        {

        }

        public override void Attack(ITarget target)
        {
            
        }
    }
}
