using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Wizards
{
    internal abstract class Enemy : Unit, IEnemy
    {
        protected IWeapon _weapon;
        public Enemy(View view, IWeaponPrototype weaponPrototype, float hp) : base(view, hp)
        {
            _weapon = weaponPrototype.SetWeapon(_view.transform.Find("Weapon")??_view.transform);
        }

        public event Action<IEnemy> OnDeath;
        public Vector2 Position
        {
            get => new Vector2(_view.transform.position.x, _view.transform.position.y);
            set => _view.Position = value;
        }
        internal override void Initiate() { }
        protected override void Die() { OnDeath.Invoke(this); GameObject.Destroy(_view.gameObject); _view = null; }
        public abstract void MoveTo(Vector2 destination);
        public virtual void Attack(ITarget target)
        {
            if(_weapon.Ready) _weapon.Attack(target);
        }

        public void DealDamage(float damage)
        {
            _hp -= damage;
            if (_hp <= 0) Die();
        }

        protected override void OnCollision(Collider2D collider)
        {
            if (collider.transform.GetComponent<View>() is Bullet b && b.Shooter.Equals("Player")) DealDamage(b.Damage);
        }
    }
}
