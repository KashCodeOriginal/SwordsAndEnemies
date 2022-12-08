using System;
using Units.Enemy.Logic;
using UnityEngine;

namespace Spawners.Loot
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;

        private void Start()
        {
            _enemyDeath.IsEnemyDied += SpawnLoot;
        }

        private void SpawnLoot()
        {
            
        }
    }
}
