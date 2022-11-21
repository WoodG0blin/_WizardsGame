using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Wizards
{
    internal class RangedEnemy : Enemy
    {
        protected float _speed = 0.2f;
        protected float _attackDistance = 2f;

        public Transform wand { get => _view.transform.Find("Wand"); }


        public RangedEnemy(View view, IWeaponPrototype weaponPrototype, float hp) : base(view, weaponPrototype, hp) { }
        public override void MoveTo(Vector2 destination)
        {
            _view.Position = CalculatePosition(destination);
            _view.Direction = destination - Position;
        }

        private Vector2 CalculatePosition(Vector2 original)
        {
            Vector2 direction = (original - Position).normalized;
            Vector2 newDestination = original - direction * _attackDistance;

            return Position + (newDestination - Position).normalized * _speed * Time.deltaTime;
        }
    }
}
