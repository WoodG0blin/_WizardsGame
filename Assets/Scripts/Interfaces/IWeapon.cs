using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    public interface IWeapon
    {
        public Vector2 Position { get; }
        public Vector2 Direction { get; }

        public bool Ready { get; }
        public void Attack(ITarget target);
    }
}