using Data.Player;
using Infrastructure.Factory.EnemyFactory;
using Services.PersistentProgress;
using Units.Enemy.Logic;
using UnityEngine;

namespace Spawners
{
    public class SpawnPoint : MonoBehaviour, IProgressSavable
    {
        [SerializeField] private MonsterTypeId _monsterTypeId;
        public string UniqueId { get; private set; }

        [SerializeField] private bool _slain;

        private IEnemyFactory _enemyFactory;
        
        private EnemyDeath _enemyDeath;

        public void Construct(IEnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }
        

        public void SetUp(MonsterTypeId monsterTypeId, string spawnerId)
        {
            _monsterTypeId = monsterTypeId;
            UniqueId = spawnerId;
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            if (_slain)
            {
                playerProgress.KillData.ClearedSpawners.Add(UniqueId);
            }
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            if(playerProgress.KillData.ClearedSpawners.Contains(UniqueId))
            {
                _slain = true;
            }
            else
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            var monster = _enemyFactory.CreateInstance(_monsterTypeId, transform);
            
            if (monster.TryGetComponent(out EnemyDeath enemyDeath))
            {
                _enemyDeath = enemyDeath;
                
                enemyDeath.IsEnemyDied += Slay;
            }
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