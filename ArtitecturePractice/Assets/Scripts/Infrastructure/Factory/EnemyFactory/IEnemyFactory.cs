using System.Threading.Tasks;
using Services;
using Spawners.Enemy;
using UnityEngine;

namespace Infrastructure.Factory.EnemyFactory
{
    public interface IEnemyFactory : IEnemyFactoryInfo, IService
    {
        public Task<GameObject> CreateInstance(MonsterTypeId monsterTypeId, Transform transform);

        public void DestroyInstance(GameObject instance);

        public void DestroyAllInstances();
    }
}