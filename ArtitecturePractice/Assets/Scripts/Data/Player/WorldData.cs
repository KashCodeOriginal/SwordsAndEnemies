using System;

namespace Data.Player
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;

        public WorldData(string sceneName)
        {
            PositionOnLevel = new PositionOnLevel(sceneName);
        }
    }
}