using Data.Static;
using Spawners.Enemy;
using UI.Services.WindowsService;
using UnityEngine;

namespace Services.StaticData
{
    public interface IStaticDataService : IService
    {
        public void LoadStaticData();
        public MonsterStaticData GetMonsterData(MonsterTypeId monsterTypeId);
        public LevelStaticData ForLevel(string key);
        public WindowConfig ForWindow(WindowID windowID);
    }
}