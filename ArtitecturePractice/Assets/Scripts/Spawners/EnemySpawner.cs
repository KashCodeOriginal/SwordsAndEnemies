using System;
using Data.Player;
using Services.PersistentProgress;
using UnityEngine;

namespace Spawners
{
    public class EnemySpawner : MonoBehaviour, IProgressSavable
    {
        [SerializeField] private MonsterTypeId _monsterTypeId;
        [SerializeField] private string _uniqueId;

        [SerializeField] private bool _slain;

        private void Awake()
        {
            _uniqueId = GetComponent<UniqueID>().Id;
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

        private void Spawn()
        {
        }
    }
} 