using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    public class WandPrototype : IWeaponPrototype
    {
        protected SO_WeaponConfig _config;
        public WandPrototype(SO_WeaponConfig config)
        {
            _config = config;
        }

        public IWeapon SetWeapon(Transform parent)
        {
            return new Wand(Services.GetService<Pool<Bullet>>(), _config, parent);
        }
    }
}