using System.Collections.Generic;
using System.Threading.Tasks;
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
        public PlayerFactory(ISaveLoadInstancesWatcher saveLoadInstancesWatcher)
        {
            _saveLoadInstancesWatcher = saveLoadInstancesWatcher;
        }

        public GameObject PlayerInstance { get; private set; }
        
        private readonly ISaveLoadInstancesWatcher _saveLoadInstancesWatcher;

        public GameObject CreatePlayer(GameObject prefab, Vector3 position)
        {
            var instance = InstantiateRegistered(prefab, position);

            PlayerInstance = instance;

            return instance;
        }

        public void DestroyPlayer()
        {
            Object.Destroy(PlayerInstance);
        }

        private GameObject InstantiateRegistered(GameObject prefab, Vector3 position)
        {
            var instance = Object.Instantiate(prefab, position, Quaternion.identity);
                
            _saveLoadInstancesWatcher.RegisterProgress(instance);

            return instance;
        }
    }
}


