using System;
using UnityEngine;

namespace Services.StaticData
{
    [Serializable]
    public class EnemySpawnerData
    {
        public EnemySpawnerData(string id, MonsterTypeId monsterTypeID, Vector3 position)
        {
            ID = id;
            MonsterTypeID = monsterTypeID;
            Position = position;
        }

        public string ID;
        public MonsterTypeId MonsterTypeID;
        public Vector3 Position;
    }
}