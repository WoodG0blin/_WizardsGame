using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    internal abstract class Unit
    {
        protected View _view;
        protected float _hp; // TODO: replace with Stats class

        internal Unit(View view, float hp)
        {
            _view = view;
            _view.OnCollision += OnCollision;
            _hp = hp;
        }

        internal abstract void Initiate();
        protected abstract void Die();
        protected virtual void OnCollision(Collider2D collider) { }
    }
}
