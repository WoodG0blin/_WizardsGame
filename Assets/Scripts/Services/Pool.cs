using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    public class Pool<T> : IService where T : View
    {
        protected Stack<T> _pool;
        protected int _number;
        protected Transform _root;
        protected GameObject _prefab;

        public Pool(GameObject prefab, Transform root, int number)
        {
            _prefab = prefab;
            _prefab.SetActive(false);
            _root = root;
            _number = number;
            _pool = new Stack<T>();
            FillPool(true);
        }


        public IService Instance { get => this; }

        private void FillPool(bool full)
        {
            T item;

            if (_pool.Count < _number)
            {
                GameObject temp = GameObject.Instantiate(_prefab);
                temp.SetActive(false);
                temp.transform.SetParent(_root);
                if(!temp.TryGetComponent<T>(out item)) item = temp.AddComponent<T>();
                _pool.Push(item);
                if(full) FillPool(full);
            }
        }

        public T Get(Transform newParent)
        {
            if (_pool.Count == 0) FillPool(false);
            T temp = _pool.Pop();
            temp.transform.SetParent(newParent);
            temp.transform.localPosition = Vector3.zero;
            temp.gameObject.SetActive(true);
            return temp;
        }
        public T Get() { return Get(null); }

        public T GetAt(Vector3 position)
        {
            T temp = Get();
            temp.transform.position = position;
            return temp;
        }

        public void Return(T item)
        {
            if (_pool.Count < _number)
            {
                item.gameObject.SetActive(false);
                item.transform.SetParent(_root);
                _pool.Push(item);
            }
            else GameObject.Destroy(item.gameObject);
        }
            
    }
}
