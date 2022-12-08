using Data.Player;
using Infrastructure.Factory.EnemyFactory;
using Services.PersistentProgress;
using Services.ServiceLocator;
using Units.Enemy.Logic;
using UnityEngine;

namespace Spawners
{
    public class EnemySpawner : MonoBehaviour, IProgressSavable
    {
        [SerializeField] private MonsterTypeId _monsterTypeId;
        [SerializeField] private string _uniqueId;

        [SerializeField] private bool _slain;

        private IEnemyFactory _enemyFactory;
        
        private EnemyDeath _enemyDeath;

        private void Awake()
        {
            _uniqueId = GetComponent<UniqueID>().Id;
            _enemyFactory = AllServices.Container.Single<IEnemyFactory>();
        }


        public void UpdateProgress(PlayerProgress playerProgress)
        {
            if (_slain)
            {
                playerProgress.KillData.ClearedSpawners.Add(_uniqueId);
            }
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            if(playerProgress.KillData.ClearedSpawners.Contains(_uniqueId))
            {
                _slain = true;
            }
            else
            {
                Spawn();
            }
        }

        private GameObject Spawn()
        {
            var monster = _enemyFactory.CreateInstance(_monsterTypeId, transform);

            if (monster.TryGetComponent(out EnemyDeath enemyDeath))
            {
                _enemyDeath = enemyDeath;
                
                enemyDeath.IsEnemyDied += Slay;
            }
            
            return monster;
        }

        private void Slay()
        {
            if (_enemyDeath != null)
            {
                _enemyDeath.IsEnemyDied -= Slay;
            }
            
            _slain = true;
        }
    }
} 