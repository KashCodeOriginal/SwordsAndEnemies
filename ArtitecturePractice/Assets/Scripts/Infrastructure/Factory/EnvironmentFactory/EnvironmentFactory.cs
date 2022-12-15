using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Factory.EnvironmentFactory
{
    public class EnvironmentFactory : IEnvironmentFactory
    {
        private List<GameObject> _instances = new List<GameObject>();

        public IReadOnlyList<GameObject> Instances
        {
            get => _instances;
        }

        public GameObject CreateInstance(GameObject prefab)
        {
            var instance = Object.Instantiate(prefab);
            
            _instances.Add(instance);

            return instance;
        }

        public void DestroyInstance(GameObject instance)
        {
            if (_instances.Contains(instance))
            {
                _instances.Remove(instance);
            }
        }
    }
}