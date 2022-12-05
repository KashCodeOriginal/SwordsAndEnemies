using Services;
using UnityEngine;

namespace Infrastructure.Factory.EnemyFactory
{
    public interface IEnemyFactory : IEnemyFactoryInfo, IService
    {
        public GameObject CreateInstance(string prefabPath);

        public void DestroyInstance(GameObject instance);

        public void DestroyAllInstances();
    }
}