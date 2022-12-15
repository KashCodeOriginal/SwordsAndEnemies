using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Services.AssetsProvider
{
    public interface IAddressableAssetProvider : IService
    {
        public void Initialize();
        public Task<T> GetAsset<T>(string address) where T : Object;
        public Task<T> GetAsset<T>(AssetReference assetReference) where T : Object;
        public void CleanUp();
    }
}