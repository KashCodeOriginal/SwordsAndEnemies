using UnityEngine;

namespace Spawners.Enemy
{
    public class SpawnMarker : MonoBehaviour
    {
        [SerializeField] private MonsterTypeId _monsterTypeId;

        public MonsterTypeId MonsterTypeId => _monsterTypeId;
    }
}