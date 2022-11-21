using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    public class SwordPrototype : IWeaponPrototype
    {
        protected SO_WeaponConfig _config;
        public SwordPrototype(SO_WeaponConfig config) { _config = config; }

        public IWeapon SetWeapon(Transform parent)
        {
            return new Sword(_config, parent);
        }
    }
}

