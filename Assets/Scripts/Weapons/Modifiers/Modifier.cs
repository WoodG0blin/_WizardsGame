using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    public abstract class Modifier : IModifier
    {
        public enum Type { aim, boost, fault }
        protected Type _type;
        public Type type { get => _type; }
        public abstract void SetModifier(ModifiedWeapon weapon);
        public abstract void RemoveModifier(ModifiedWeapon weapon);
    }
}
