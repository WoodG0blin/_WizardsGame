using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    public interface IModifier
    {
        public Modifier.Type type { get; }
        public void SetModifier(ModifiedWeapon weapon);
        public void RemoveModifier(ModifiedWeapon weapon);
    }
}
