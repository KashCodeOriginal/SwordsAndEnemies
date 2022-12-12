using Data.Assets;
using UnityEngine;
using Units.Enemy.Logic;
using Watchers.SaveLoadWatchers;
using Infrastructure.Factory.EnvironmentFactory;
using Services.PersistentProgress;

namespace Spawners.Loot
{
    public class LootSpawner : MonoBehaviour
    {
        public void Construct(IEnvironmentFactory environmentFactory, 
            ISaveLoadInstancesWatcher saveLoadInstancesWatcher, 
            IPersistentProgressService persistentProgressService)
        {
            _environmentFactory = environmentFactory;
            _saveLoadInstancesWatcher = saveLoadInstancesWatcher;
            _persistentProgressService = persistentProgressService;
        }

        [SerializeField] private EnemyDeath _enemyDeath;

        private IEnvironmentFactory _environmentFactory;

        private ISaveLoadInstancesWatcher _saveLoadInstancesWatcher;

        private IPersistentProgressService _persistentProgressService;

        private int _lootMin;

        private int _lootMax;

        private void Start()
        {
            _enemyDeath.IsEnemyDied += SpawnLoot;
        }

        private void SpawnLoot()
        {
            var instance = _environmentFactory.CreateInstance(AssetsConstants.LOOT);
            
            instance.transform.position = transform.position;
            
            var lootItem = GenerateLootAmount(); 

            if (instance.TryGetComponent(out LootPiece lootPiece))
            {
                lootPiece.Construct(_persistentProgressService.PlayerProgress.WorldData);
                
                lootPiece.Initialize(lootItem);
            }

            _saveLoadInstancesWatcher.RegisterProgress(instance);
        }

        private Loot GenerateLootAmount()
        {
            return new Loot()
            {
                Value = Random.Range(_lootMin, _lootMax)
            };
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}
