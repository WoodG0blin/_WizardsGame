using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    internal class MeleeEnemy : Enemy
    {
        protected float _speed = 0.4f;
        protected float _attackDistance = 1.2f;

        public MeleeEnemy(View view, IWeaponPrototype weaponPrototype, float hp) : base(view, weaponPrototype, hp) { }
        public override void MoveTo(Vector2 destination)
        {
            _view.Position = CalculatePosition(destination);
            _view.Direction = destination - Position;
        }

        private Vector2 CalculatePosition(Vector2 original)
        {
            Vector2 direction = (original - Position).normalized;
            Vector2 newDestination = original - direction * _attackDistance;

            return ((newDestination - Position).magnitude > (direction * _speed * Time.deltaTime).magnitude) ? Position + direction * _speed * Time.deltaTime : newDestination;
        }
    }
}
