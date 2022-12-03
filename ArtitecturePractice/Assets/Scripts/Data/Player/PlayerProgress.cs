using System;

namespace Data.Player
{
    [Serializable]
    public class PlayerProgress
    {
        public PlayerProgress(string sceneName)
        {
            WorldData = new WorldData(sceneName);
        }

        public WorldData WorldData;
    }
}
