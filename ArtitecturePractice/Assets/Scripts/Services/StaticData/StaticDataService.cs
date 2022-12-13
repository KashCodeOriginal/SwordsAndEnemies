using System.Collections.Generic;
using System.Linq;
using Data.Static;
using Spawners.Enemy;
using UI.Services.WindowsService;
using UnityEngine;

namespace Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string MONSTERS_STATIC_DATA_PATH = "Data/Static/Monsters";
        private const string ENEMY_STATIC_DATA_PATH = "Data/Static/Levels";
        private const string WINDOW_STATIC_DATA_PATH = "Data/Static/Windows/WindowStaticData";
        
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<WindowID,WindowConfig> _windows;

        public void LoadStaticData()
        {
            _monsters = Resources
                .LoadAll<MonsterStaticData>(MONSTERS_STATIC_DATA_PATH)
                .ToDictionary(x => x.MonsterTypeId, x => x);
            
            _levels = Resources
                .LoadAll<LevelStaticData>(ENEMY_STATIC_DATA_PATH)
                .ToDictionary(x => x.LevelKey, x => x);
            
            _windows = Resources
                .Load<WindowsStaticData>(WINDOW_STATIC_DATA_PATH)
                .WindowConfigs
                .ToDictionary(x => x.WindowID, x => x);
        }

        public MonsterStaticData GetMonsterData(MonsterTypeId monsterTypeId)
        {
            return _monsters.TryGetValue(monsterTypeId, out MonsterStaticData staticData) ? staticData : null;
        }

        public LevelStaticData ForLevel(string key)
        {
            return _levels.TryGetValue(key, out LevelStaticData staticData) ? staticData : null;
        }

        public WindowConfig ForWindow(WindowID windowID)
        {
            return _windows.TryGetValue(windowID, out WindowConfig staticData) ? staticData : null;
        }
    }
}