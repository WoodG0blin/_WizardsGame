using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    public interface IWeaponPrototype
    {
        IWeapon SetWeapon(Transform parent);
    }
}
