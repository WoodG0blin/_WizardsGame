using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    public interface IExecutor
    {
        void AddExecutable(IExecutable executable);
    }
}
