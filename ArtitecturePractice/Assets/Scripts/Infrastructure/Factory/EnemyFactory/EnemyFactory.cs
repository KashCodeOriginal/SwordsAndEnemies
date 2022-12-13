using System.Collections.Generic;
using Data.Static;
using Infrastructure.Factory.EnvironmentFactory;
using Infrastructure.Factory.PlayerFactory;
using Pathfinding;
using Services.PersistentProgress;
using Services.StaticData;
using Spawners.Enemy;
using Spawners.Loot;
using Units.Base;
using Units.Enemy.Logic;
using UnityEngine;
using Watchers.SaveLoadWatchers;

namespace Infrastructure.Factory.EnemyFactory
{
    public class EnemyFactory : IEnemyFactory
    {
        public EnemyFactory(IStaticDataService staticDataService, 
            IPlayerFactory playerFactory, 
            IEnvironmentFactory environmentFactory, 
            ISaveLoadInstancesWatcher saveLoadInstancesWatcher,
            IPersistentProgressService persistentProgressService)
        {
            _staticDataService = staticDataService;
            _playerFactory = playerFactory;
            _environmentFactory = environmentFactory;
            _saveLoadInstancesWatcher = saveLoadInstancesWatcher;
            _persistentProgressService = persistentProgressService;
        }

        public IReadOnlyList<GameObject> Instances => _instances;

        private readonly IStaticDataService _staticDataService;
        private readonly IPlayerFactory _playerFactory;
        private readonly IEnvironmentFactory _environmentFactory;
        private readonly ISaveLoadInstancesWatcher _saveLoadInstancesWatcher;
        private readonly IPersistentProgressService _persistentProgressService;

        private List<GameObject> _instances = new List<GameObject>();

        public GameObject CreateInstance(MonsterTypeId monsterTypeId, Transform transform)
        {
            var monsterStaticData = _staticDataService.GetMonsterData(monsterTypeId);
            
            var instance = InstantiateInstance(monsterStaticData.Prefab, transform);
            
            SetUp(instance, monsterStaticData);

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

        private GameObject InstantiateInstance(GameObject prefab, Transform parent)
        {
            var enemyInstance = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);
            
            _instances.Add(enemyInstance);

            return enemyInstance;
        }

        private void SetUp(GameObject instance, MonsterStaticData monsterStaticData)
        {
            if (instance.TryGetComponent(out IHealth health))
            {
                health.SetUp(monsterStaticData.HealthPoints, monsterStaticData.MaxHealthPoints);
            }

            if (instance.TryGetComponent(out AIPath aiPath))
            {
                aiPath.maxSpeed = monsterStaticData.MovementSpeed;
            }

            if (instance.TryGetComponent(out EnemyAttack enemyAttack))
            {
                enemyAttack.SetUp(monsterStaticData.Damage, monsterStaticData.AttackCooldown, monsterStaticData.Cleavage, monsterStaticData.EffectiveDistance);
            }
            
            if (instance.TryGetComponent(out MoveToPlayer moveToPlayer))
            {
                moveToPlayer.SetTarget(_playerFactory.PlayerInstance.transform);
            }
            
            if (instance.TryGetComponent(out RotateToPlayer rotateToPlayer))
            {   
                rotateToPlayer.SetPlayer(_playerFactory.PlayerInstance.transform);
            }

            if (instance.TryGetComponent(out EnemyAttack attack))
            {
                attack.SetUpPlayer(_playerFactory.PlayerInstance.transform);
            }

            var lootSpawner = instance.GetComponentInChildren<LootSpawner>();
            lootSpawner.Construct(_environmentFactory, _saveLoadInstancesWatcher, _persistentProgressService);
            lootSpawner.SetLoot(monsterStaticData.MinLoot, monsterStaticData.MaxLoot);
        }
    }
}
