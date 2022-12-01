using Data;
using Data.Assets;
using UnityEngine;
using Services.AssetsProvider;

namespace Infrastructure.Factory.PlayerFactory
{
    public class PlayerFactory : IPlayerFactory
    {
        public PlayerFactory(IAssetsProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }
        public GameObject PlayerInstance { get; private set; }
        
        private readonly IAssetsProvider _assetsProvider;

        public GameObject CreatePlayer()
        {
            var instance = Instantiate(AssetsConstants.PLAYER_PREFAB_PATH, new Vector3(10, 1, 10));

            PlayerInstance = instance;

            return instance;
        }

        public void DestroyPlayer()
        {
            Object.Destroy(PlayerInstance);
        }

        private GameObject Instantiate(string path, Vector3 position)
        {
            var prefab = _assetsProvider.GetAssetByPath<GameObject>(path);
            
            var instance = Object.Instantiate(prefab, position, Quaternion.identity);

            return instance;
        }
    }
}


