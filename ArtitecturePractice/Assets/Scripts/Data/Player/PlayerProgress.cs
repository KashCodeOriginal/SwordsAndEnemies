using System;
using System.Collections.Generic;

namespace Data.Player
{
    [Serializable]
    public class PlayerProgress
    {
        public PlayerProgress(string sceneName)
        {
            WorldData = new WorldData(sceneName);
            HeroState = new HeroState();
            HeroStats = new HeroStats();
            KillData = new KillData();
            PurchaseData = new PurchaseData();
        }

        public WorldData WorldData;
        public HeroState HeroState;
        public HeroStats HeroStats;
        public KillData KillData;
        public PurchaseData PurchaseData;
    }

    [Serializable]
    public class KillData
    {
        public List<string> ClearedSpawners = new List<string>();
    }
}
