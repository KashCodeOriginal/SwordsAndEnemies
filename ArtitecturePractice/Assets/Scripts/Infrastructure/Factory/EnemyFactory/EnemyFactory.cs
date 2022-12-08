using System.Collections.Generic;
using Data.Static;
using Infrastructure.Factory.PlayerFactory;
using Pathfinding;
using Services.StaticData;
using Units.Enemy.Logic;
using UnityEngine;

namespace Infrastructure.Factory.EnemyFactory
{
    public class EnemyFactory : IEnemyFactory
    {
        public EnemyFactory(IStaticDataService staticDataService, IPlayerFactory playerFactory)
        {
            _staticDataService = staticDataService;
            _playerFactory = playerFactory;
        }

        public IReadOnlyList<GameObject> Instances => _instances;

        private readonly IStaticDataService _staticDataService;
        private readonly IPlayerFactory _playerFactory;

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
        }
    }
}
