using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

namespace Wizards
{
    public abstract class View : MonoBehaviour
    {
        protected Transform _transform;
        protected SpriteRenderer _spriteRenderer;
        protected Collider2D _collider;

        public event Action<Collider2D> OnCollision;

        private void Awake()
        {
            _transform = transform;
            if(!TryGetComponent<SpriteRenderer>(out _spriteRenderer)) _spriteRenderer = _transform.AddComponent<SpriteRenderer>();
            if(!TryGetComponent<Collider2D>(out _collider)) _collider = _transform.AddComponent<CircleCollider2D>();
        }

        public Vector2 Position
        {
            get => CoordinatesModel.GetVector2(_transform.position);
            set
            {
                Vector2 temp = CoordinatesModel.ClampByField(value);
                _transform.position = new Vector3(temp.x, temp.y, 0);
            }
        }
        public Vector2 Direction { get => CoordinatesModel.GetVector2(_transform.up).normalized; set => transform.Rotate(0, 0, Vector2.SignedAngle(Direction, value));}
        public void Rotate(float angle) { transform.Rotate(0,0, angle); }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollision?.Invoke(collision.collider);
        }

        public View Clone()
        {
            var temp = GameObject.Instantiate(_transform.gameObject);
            temp.SetActive(true);
            return temp.GetComponent<View>();
        }
    }
}
