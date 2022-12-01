using Services;
using UnityEngine;

namespace Infrastructure.Factory.EnvironmentFactory
{
    public interface IEnvironmentFactory : IEnvironmentFactoryInfo, IService
    {
        public GameObject CreateInstance(string path);
        public void DestroyInstance(GameObject instance);
    }
}
