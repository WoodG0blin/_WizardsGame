using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Wizards
{
    public class Bullet : View
    {
        private float _damage;
        private Clock _clock;
        private Clock.Timer _timer;
        public float Damage { get => _damage; set => _damage = value; }
        public string Shooter { get; set; }

        Rigidbody2D _rigidbody2D;
        public Rigidbody2D RB2D { get => _rigidbody2D; }
        public event Action<Bullet> OnDestroy;

        private void Awake()
        {
            if(!TryGetComponent<Rigidbody2D>(out _rigidbody2D)) _rigidbody2D = transform.AddComponent<Rigidbody2D>();
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            //if (collision.collider.transform.GetComponent<View>() is ITarget t) t.DealDamage(_damage);
            _clock.RemoveTimer(_timer);
            OnDestroy?.Invoke(this);
        }

        public void SetTimer(float t)
        {
            if(_clock == null) _clock = Services.GetService<Clock>();
            _timer = _clock.SetTimer(t, () => { OnDestroy?.Invoke(this); });
        }
    }
}
