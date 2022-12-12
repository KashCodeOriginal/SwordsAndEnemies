using System.Collections.Generic;
using System.Linq;
using Data.Static;
using UnityEngine;

namespace Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;

        public void LoadMonsters()
        {
            _monsters = Resources
                .LoadAll<MonsterStaticData>("Data/Static/Monsters")
                .ToDictionary(x => x.MonsterTypeId, x => x);
            
            _levels = Resources
                .LoadAll<LevelStaticData>("Data/Static/Levels")
                .ToDictionary(x => x.LevelKey, x => x);
        }

        public MonsterStaticData GetMonsterData(MonsterTypeId monsterTypeId)
        {
            return _monsters.TryGetValue(monsterTypeId, out MonsterStaticData staticData) ? staticData : null;
        }

        public LevelStaticData ForLevel(string key)
        {
            return _levels.TryGetValue(key, out LevelStaticData staticData) ? staticData : null;
        }
    }
}