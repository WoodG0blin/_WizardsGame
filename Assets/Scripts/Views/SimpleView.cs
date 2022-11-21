using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

namespace Wizards
{
    public class SimpleView : View
    {
        public void Awake()
        {
            _transform = transform;
            if (!TryGetComponent<SpriteRenderer>(out _spriteRenderer)) _spriteRenderer = _transform.AddComponent<SpriteRenderer>();

        }

    }
}
