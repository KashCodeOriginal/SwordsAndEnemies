using UnityEngine;

namespace Services.AssetsProvider
{
    public class AssetsProvider : IAssetsProvider
    {
        public T GetAssetByPath<T>(string path) where T : Object
        {
            var prefab = Resources.Load<T>(path);

            return prefab;
        }
    }
}