using System.Collections.Generic;
using Data.Assets;
using Infrastructure.Factory.EnemyFactory;
using Services.AssetsProvider;
using Spawners;
using UnityEngine;
using Watchers.SaveLoadWatchers;

namespace Infrastructure.Factory.EnvironmentFactory
{
    public class EnvironmentFactory : IEnvironmentFactory
    {
        public EnvironmentFactory(IAssetsProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }

        private readonly IAssetsProvider _assetsProvider;

        private List<GameObject> _instances = new List<GameObject>();

        public IReadOnlyList<GameObject> Instances
        {
            get => _instances;
        }

        public GameObject CreateInstance(string path)
        {
            var prefab = _assetsProvider.GetAssetByPath<GameObject>(path);
            
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