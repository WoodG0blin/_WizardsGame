using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    internal abstract class Controller : IExecutable
    {
        private IExecutor _executor;
        protected IExecutor Executor { get => _executor; }

        public Controller(IExecutor executor)
        {
            executor.AddExecutable(this);
        }

        public abstract void Execute();
    }
}
