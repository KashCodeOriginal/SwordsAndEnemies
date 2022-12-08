using Data.Static;

namespace Services.StaticData
{
    public interface IStaticDataService : IService
    {
        public void LoadMonsters();
        public MonsterStaticData GetMonsterData(MonsterTypeId monsterTypeId);
    }
}