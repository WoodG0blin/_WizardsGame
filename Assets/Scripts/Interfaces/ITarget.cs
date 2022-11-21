using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    public interface ITarget
    {
        Vector2 Position { get; set; }
        void DealDamage(float damage);
    }
}
