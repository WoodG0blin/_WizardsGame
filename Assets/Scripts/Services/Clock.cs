using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    public class Clock : IService
    {
        public class Timer
        {
            private float _time;
            private Action _action;
            public Timer(float time, Action action)
            {
                _time = time;
                _action = action;
            }
            public bool Check(float time)
            {
                if (time > _time)
                {
                    _action();
                    _action = null;
                    return true;
                }
                return false;
            }
        }

        private float _elapsedTime;
        private bool _executing;
        private List<Timer> _timers = new List<Timer>();
        private List<Timer> _timersToAdd = new List<Timer>();
        private List<Timer> _timersToRemove = new List<Timer>();

        public IService Instance { get => this; }
        public void Tick(float deltaTime)
        {
            _elapsedTime += deltaTime;
            AddTimers();
            _executing = true;
            foreach(Timer timer in _timers)
            {
                if(timer.Check(_elapsedTime)) _timersToRemove.Add(timer);
            }
            RemoveTimers();
            _executing=false;
        }

        private void AddTimers()
        {
            foreach(Timer timer in _timersToAdd) _timers.Add(timer);
            _timersToAdd.Clear();
        }

        private void RemoveTimers()
        {
            foreach(Timer timer in _timersToRemove) _timers.Remove(timer);
            _timersToRemove.Clear();
        }

        public Timer SetTimer(float seconds, Action action)
        {
            Timer timer = new Timer(_elapsedTime + seconds, action);
            if (_executing) _timersToAdd.Add(timer); else _timers.Add(timer);
            return timer;
        }

        public void RemoveTimer(Timer timer)
        {
            _timersToRemove.Add(timer);
        }
    }
}
