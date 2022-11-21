using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Wizards
{
    public abstract class Weapon : IWeapon
    {
        public enum WeaponType { wand, sword, playerwand}

        protected readonly View _view;
        protected bool _ready;

        protected float _power;
        protected float _force;
        protected Vector2 _direction { get => _view.Direction; }
        protected Vector2 _position { get => _view.Position; }
        protected float _rangeAngle;
        protected float _rangeDistance;
        protected float _coolDown;

        protected Clock _clock;

        protected SO_WeaponConfig _config;
        protected Transform _parent;

        internal Weapon(SO_WeaponConfig config, Transform parent)
        {
            _config = config;
            _parent = parent;
            _view = GameObject.Instantiate(config.Prefab, parent).GetComponent<View>();
            _power = config.Damage;
            _force = config.Force;
            _rangeAngle = config.RangeAngle;
            _rangeDistance = config.RangeDistance;
            _coolDown = config.CoolDown;
            _clock = Services.GetService<Clock>();
            _ready = false;
            _clock.SetTimer(_coolDown, CoolDown);
        }

        public Vector2 Position { get => _view.Position; }
        public Vector2 Direction { get => _view.Direction; }
        public bool Ready { get => _ready; }
        public View Instance { get => _view; }

        public void Attack(ITarget target)
        {
            if (HasInRange(target))
            {
                _ready = false;
                AttackActions(target);
                _clock.SetTimer(_coolDown, CoolDown);
            }
        }

        protected bool HasInRange(ITarget target) { return (target != null) ? (IsInsideAngle(target) && IsInRange(target)) : true; }
        protected virtual void AttackActions(ITarget target) { }

        private bool IsInsideAngle(ITarget target) { return Vector2.Angle(_direction, target.Position - _position) <= _rangeAngle / 2; }
        private bool IsInRange(ITarget target) { return Vector2.Distance(target.Position, _position) <= _rangeDistance; }

        private void CoolDown() { _ready = true; }
    }
}

