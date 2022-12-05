using System.Collections.Generic;
using Services.AssetsProvider;
using UnityEngine;

namespace Infrastructure.Factory.EnemyFactory
{
    public class EnemyFactory : IEnemyFactory
    {
        public EnemyFactory(IAssetsProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }

        public IReadOnlyList<GameObject> Instances => _instances;

        private readonly IAssetsProvider _assetsProvider;

        private List<GameObject> _instances = new List<GameObject>();


        public GameObject CreateInstance(string prefabPath)
        {
            var instance = InstantiateInstance(prefabPath);

            return instance;
        }

        public void DestroyInstance(GameObject instance)
        {
            if (_instances.Contains(instance))
            {
                _instances.Remove(instance);
            }
        }

        public void DestroyAllInstances()
        {
            _instances.Clear();
        }

        private GameObject InstantiateInstance(string prefabPath)
        {
            var enemyPrefab = _assetsProvider.GetAssetByPath<GameObject>(prefabPath);

            var enemyInstance = Object.Instantiate(enemyPrefab);
            
            _instances.Add(enemyInstance);

            return enemyInstance;
        }
    }
}
