using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

namespace Wizards
{
    public class Services
    {
        private static Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        public static void SetService(IService value)
        {
            var type = value.GetType();
            if (!_services.ContainsKey(type) && value is IService service) _services.Add(type, service);
        }

        public static T GetService<T>() where T: class
        {
            if(_services.ContainsKey(typeof(T))) return _services[typeof(T)] as T;
            else return null;
        }
    }
}
