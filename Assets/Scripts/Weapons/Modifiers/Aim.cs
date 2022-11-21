using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    public class Aim : Modifier
    {
        protected View _aim;
        public Aim(View aim) { _aim = aim; _type = Type.aim; }

        public override void SetModifier(ModifiedWeapon weapon)
        {
            _aim = GameObject.Instantiate(_aim).GetComponent<View>();
            _aim.transform.SetParent(weapon.Construction.transform);
            _aim.transform.localPosition = new Vector3(0, _aim.transform.localScale.y / 2, 0);
            _aim.transform.localRotation = Quaternion.identity;
        }

        public override void RemoveModifier(ModifiedWeapon weapon)
        {
            _aim.transform.SetParent(null);
            GameObject.Destroy(_aim.transform.gameObject);
        }
    }
}
