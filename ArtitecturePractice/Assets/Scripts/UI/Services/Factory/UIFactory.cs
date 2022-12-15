using Data.Assets;
using Services.Ads;
using Services.AssetsProvider;
using Services.PersistentProgress;
using Services.StaticData;
using UI.Services.WindowsService;
using UI.ShopUI;
using UnityEngine;

namespace UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        public UIFactory(IAddressableAssetProvider assetsProvider, IStaticDataService staticData, IPersistentProgressService persistentProgressService, IAdsService adsService)
        {
            _assetsProvider = assetsProvider;
            _staticData = staticData;
            _persistentProgressService = persistentProgressService;
            _adsService = adsService;
        }

        private readonly IAddressableAssetProvider _assetsProvider;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IAdsService _adsService;
        private Transform _uiRootTransform;

        public async void CreateUIRoot()
        {
            var uiRootPrefab = await _assetsProvider.GetAsset<GameObject>(AssetsAddressablesConstants.UI_ROOT);
            var uiRootInstance = Object.Instantiate(uiRootPrefab);
            _uiRootTransform = uiRootInstance.transform;
        }

        public void CreateShopWindow()
        {
            var windowConfig = _staticData.ForWindow(WindowID.Shop);
            
            var windowInstance = Object.Instantiate(windowConfig.Prefab, _uiRootTransform) as ShopWindow;
            
            if (windowInstance != null && windowInstance.TryGetComponent(out ShopWindow baseWindow))
            {
                baseWindow.Construct(_adsService, _persistentProgressService);
            }

        }
    }
}