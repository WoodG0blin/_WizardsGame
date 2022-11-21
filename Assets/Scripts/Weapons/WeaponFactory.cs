using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Wizards
{
    public class WeaponFactory : IService
    {
        protected SO_WeaponsConfig _config;
        
        public WeaponFactory(SO_WeaponsConfig config)
        {
            _config = config;
        }

        public IService Instance { get => this; }

        public IWeaponPrototype CallForWeapon(Weapon.WeaponType type)
        {
            switch(type)
            {
                case Weapon.WeaponType.wand: return new WandPrototype(_config[type]);
                case Weapon.WeaponType.sword: return new SwordPrototype(_config[type]);
                case Weapon.WeaponType.playerwand: return new WandPrototype(_config[type]);
                default: return new WandPrototype(_config[type]);
            }
        }

        public virtual IWeapon SetWeapon(Transform parent) { return null; }

        public IWeapon ModifyWeapon(IWeapon weapon, Modifier.Type mode)
        {
            IModifier modifier = null;
            
            switch (mode)
            {
                case Modifier.Type.aim: modifier = new Aim(_config.Aim); break;
                case Modifier.Type.boost: modifier = new Boost(2); break;
                case Modifier.Type.fault: modifier = new Jam(0.7f); break;
                default: break;
            }

            if (weapon is ModifiedWeapon mweapon)
            {
                mweapon.AddModifier(modifier);
                weapon = mweapon;
            }
            else
            {
                var v = new ModifiedWeapon(weapon as Weapon);
                v.AddModifier(modifier);
                weapon = v;
            }

            return weapon;
        }

        public IWeapon RemoveModification(ModifiedWeapon weapon, Modifier.Type mode)
        {
            weapon.RemoveModifier(mode);
            return weapon.Modified ? weapon : weapon.BaseWeapon;
        }
    }
}
