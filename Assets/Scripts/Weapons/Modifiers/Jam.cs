using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Wizards
{
    public class Jam : Modifier
    {
        protected float _probability;
        public Jam(float probability) { _probability = Math.Clamp(probability, 0, 1); _type = Type.fault; }

        public override void SetModifier(ModifiedWeapon weapon)
        {
            weapon.OnAttack += JammedAttack;
        }

        public override void RemoveModifier(ModifiedWeapon weapon)
        {
            weapon.OnAttack -= JammedAttack;
        }

        protected void JammedAttack(IWeapon weapon, ITarget target)
        {
            if(Random.Range(0.0f, 1.0f) < _probability) weapon.Attack(target);
        }
    }
}
