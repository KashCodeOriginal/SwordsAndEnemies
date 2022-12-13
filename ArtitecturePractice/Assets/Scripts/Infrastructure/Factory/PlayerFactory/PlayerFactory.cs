using System.Collections.Generic;
using Data;
using Data.Assets;
using UnityEngine;
using Services.AssetsProvider;
using Services.StaticData;
using Watchers.SaveLoadWatchers;

namespace Infrastructure.Factory.PlayerFactory
{
    public class PlayerFactory : IPlayerFactory
    {
        public PlayerFactory(IAssetsProvider assetsProvider, ISaveLoadInstancesWatcher saveLoadInstancesWatcher, IStaticDataService staticDataService)
        {
            _assetsProvider = assetsProvider;
            _saveLoadInstancesWatcher = saveLoadInstancesWatcher;
            _staticDataService = staticDataService;
        }

        public GameObject PlayerInstance { get; private set; }
        
        private readonly IAssetsProvider _assetsProvider;
        private readonly ISaveLoadInstancesWatcher _saveLoadInstancesWatcher;
        private readonly IStaticDataService _staticDataService;

        public GameObject CreatePlayer(Vector3 position)
        {
            var instance = InstantiateRegistered(AssetsConstants.PLAYER_PREFAB_PATH, position);

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


