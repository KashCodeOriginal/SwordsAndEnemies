using Data.Assets;
using Infrastructure.Factory.EnemyFactory;
using Services.AssetsProvider;
using Spawners;
using UnityEngine;
using Watchers.SaveLoadWatchers;

namespace Infrastructure.Factory.SpawnersFactory
{
    public class SpawnerFactory : ISpawnerFactory
    {
        public SpawnerFactory(IAssetsProvider assetsProvider, ISaveLoadInstancesWatcher saveLoadInstancesWatcher, IEnemyFactory enemyFactory)
        {
            _assetsProvider = assetsProvider;
            _saveLoadInstancesWatcher = saveLoadInstancesWatcher;
            _enemyFactory = enemyFactory;
        }

        private readonly IAssetsProvider _assetsProvider;
        private readonly ISaveLoadInstancesWatcher _saveLoadInstancesWatcher;
        private readonly IEnemyFactory _enemyFactory;

        public void CreateSpawner(Vector3 dataPosition, string spawnerID, MonsterTypeId dataMonsterTypeID)
        {
            var prefab = _assetsProvider.GetAssetByPath<GameObject>(AssetsConstants.SPAWNER);
            
            var instance = Object.Instantiate(prefab, dataPosition, Quaternion.identity);
            
            _saveLoadInstancesWatcher.RegisterProgress(instance);

            if (instance.TryGetComponent(out SpawnPoint enemySpawner))
            {
                enemySpawner.Construct(_enemyFactory);
                enemySpawner.SetUp(dataMonsterTypeID, spawnerID);
            }
        }
    }
}