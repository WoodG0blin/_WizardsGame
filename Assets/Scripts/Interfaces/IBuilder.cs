using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    public interface IBuilder
    {
        IBuilder Get { get; }
    }
}
