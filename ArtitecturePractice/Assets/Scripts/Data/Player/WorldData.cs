using System;

namespace Data.Player
{
    [Serializable]
    public class WorldData
    {
        public WorldData(string sceneName)
        {
            PositionOnLevel = new PositionOnLevel(sceneName);
            LootData = new LootData();
        }

        public PositionOnLevel PositionOnLevel;

        public LootData LootData;
    }
}