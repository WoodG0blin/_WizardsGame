using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Wizards
{
    public class Stats
    {
        protected float _health;

        public Stats(float health) { _health = health; }

        public float Health
        {
            get => _health;
            protected set
            {
                _health = value > 0 ? value : 0;
                if (_health <= 0) Die();
            }
        }

        public event Action OnDeath;

        public void ReceiveDamage(float damage) { Health -= damage; }

        private void Die() { OnDeath?.Invoke(); }
    }
}
