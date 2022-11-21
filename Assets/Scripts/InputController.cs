using UnityEngine;
using System;

namespace Wizards
{
    public sealed class InputController
    {
        public event Action<Inputs> OnInputsChange;
        private Inputs current;

        public struct Inputs
        {
            public float Rotation;
            public float Forward;
            public float Dodge;
            public bool Accelerate;
            public bool Decelerate;
            public bool Fire;

            public Inputs Default
            {
                get
                {
                    Rotation = 0;
                    Forward = 0;
                    Dodge = 0;
                    Accelerate = false;
                    Decelerate = false;
                    Fire = false;
                    return this;
                }
            }
        }

        public InputController()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }

        public void Update()
        {
            current = current.Default;

            current.Rotation = Input.GetAxis("Mouse X");
            current.Forward = Input.GetAxis("Vertical");
            if(Input.GetAxis("Horizontal") > 0.1f) current.Dodge = 1;
            if(Input.GetAxis("Horizontal") < -0.1f) current.Dodge = -1;

            if (Input.GetKeyDown(KeyCode.LeftShift)) current.Accelerate = true;
            if (Input.GetKeyUp(KeyCode.LeftShift)) current.Decelerate = true;

            if (Input.GetButtonDown("Fire1")) current.Fire = true;

            OnInputsChange?.Invoke(current);
        }
    }
}
