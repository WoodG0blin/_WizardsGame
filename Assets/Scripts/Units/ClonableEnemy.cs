using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Wizards
{
    internal class ClonableEnemy : MeleeEnemy
    {
        protected List<IEnemy> _clones;
        protected bool _original;
        protected IWeaponPrototype _weaponPrototype;
                
        protected float timeToClone = 5;
        protected float elapsedTime;

        public ClonableEnemy(View view, IWeaponPrototype weaponPrototype, float hp, bool original) : base(view, weaponPrototype, hp)
        {
            _weaponPrototype = weaponPrototype;
            _clones = new List<IEnemy>();
            _original = original;
        }

        public Vector2 Direction
        {
            get => CoordinatesModel.GetVector2(_view.transform.up);
            set => _view.transform.Rotate(0, 0, Vector2.SignedAngle(CoordinatesModel.GetVector2(_view.transform.up), value));
        }

        public override void MoveTo(Vector2 destination)
        {
            if (_original)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime > timeToClone)
                {
                    elapsedTime = 0;
                    _clones.Add(Clone());
                }
            }

            base.MoveTo(destination);
            foreach (IEnemy enemy in _clones) enemy.MoveTo(destination);
        }

        public override void Attack(ITarget target)
        {
            base.Attack(target);
            foreach(IEnemy enemy in _clones) enemy.Attack(target);
        }

        internal IEnemy Clone()
        {
            var temp = new ClonableEnemy(_view.Clone(), _weaponPrototype, _hp, false);
            temp.Position = (_clones.Count > 0 ? _clones[_clones.Count - 1].Position : Position) - Direction.normalized;
            temp.Direction = Direction;
            temp.OnDeath += OnCloneDeath;
            return temp;
        }

        private void OnCloneDeath(IEnemy clone)
        {
            _clones.Remove(clone);
            if (_clones.Count == 0) Die();
        }

        protected override void OnCollision(Collider2D collider)
        {
            if (collider.transform.GetComponent<View>() is ITarget target) target.DealDamage(30);
            if (_clones.Count == 0) Die();
            else _view.gameObject.SetActive(false);
        }
    }
}
