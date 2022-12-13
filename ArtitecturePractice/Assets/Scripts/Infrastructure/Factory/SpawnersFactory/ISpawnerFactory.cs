using Services;
using Spawners.Enemy;
using UnityEngine;

namespace Infrastructure.Factory.SpawnersFactory
{
    public interface ISpawnerFactory : IService
    {
        public void CreateSpawner(Vector3 dataPosition, string spawnerID, MonsterTypeId dataMonsterTypeID);
    }
}

