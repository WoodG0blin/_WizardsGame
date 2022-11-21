using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    interface IEnemy : ITarget
    {
        void MoveTo(Vector2 destination);
        void Attack(ITarget target);
        event Action<IEnemy> OnDeath;
    }
}
