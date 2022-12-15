using System.Threading.Tasks;
using Services;
using UnityEngine;

namespace Infrastructure.Factory.EnvironmentFactory
{
    public interface IEnvironmentFactory : IEnvironmentFactoryInfo, IService
    {
        public GameObject CreateInstance(GameObject prefab);
        public void DestroyInstance(GameObject instance);
    }
}
