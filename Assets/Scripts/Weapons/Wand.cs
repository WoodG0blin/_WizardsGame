using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    public class Wand : Weapon
    {
        protected Pool<Bullet> _spells;
        public Wand(Pool<Bullet> spells, SO_WeaponConfig config, Transform parent) : base(config, parent)
        {
            _spells = spells;
        }

        protected override void AttackActions(ITarget target)
        {
            Bullet temp = _spells.GetAt(_view.Position);
            temp.Damage = _power;
            temp.Shooter = _parent.parent.tag;
            temp.OnDestroy += OnBulletDestroy;
            temp.RB2D.AddForce(Direction * _force, ForceMode2D.Impulse);
            temp.SetTimer(3.0f);
        }

        private void OnBulletDestroy(Bullet b)
        {
            b.OnDestroy -= OnBulletDestroy;
            _spells.Return(b);
        }
    }
}
