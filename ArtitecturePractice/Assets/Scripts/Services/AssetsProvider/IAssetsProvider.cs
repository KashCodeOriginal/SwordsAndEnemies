using UnityEngine;

namespace Services.AssetsProvider
{
    public interface IAssetsProvider : IService
    {
        public T GetAssetByPath<T>(string path) where T : Object;
    }
}