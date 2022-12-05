using System.Collections.Generic;
using Data;
using Data.Assets;
using UnityEngine;
using Services.AssetsProvider;
using Watchers.SaveLoadWatchers;

namespace Infrastructure.Factory.PlayerFactory
{
    public class PlayerFactory : IPlayerFactory
    {
        public PlayerFactory(IAssetsProvider assetsProvider, ISaveLoadInstancesWatcher saveLoadInstancesWatcher)
        {
            _assetsProvider = assetsProvider;
            _saveLoadInstancesWatcher = saveLoadInstancesWatcher;
        }

        public GameObject PlayerInstance { get; private set; }
        
        private readonly IAssetsProvider _assetsProvider;
        private readonly ISaveLoadInstancesWatcher _saveLoadInstancesWatcher;

        public GameObject CreatePlayer()
        {
            var instance = InstantiateRegistered(AssetsConstants.PLAYER_PREFAB_PATH, new Vector3(10, 1, 10));

            PlayerInstance = instance;

            return instance;
        }

        public void DestroyPlayer()
        {
            Object.Destroy(PlayerInstance);
        }

        private GameObject InstantiateRegistered(string path, Vector3 position)
        {
            var prefab = _assetsProvider.GetAssetByPath<GameObject>(path);

            var instance = Object.Instantiate(prefab, position, Quaternion.identity);
                
            _saveLoadInstancesWatcher.RegisterProgress(instance);

            return instance;
        }
    }
}


