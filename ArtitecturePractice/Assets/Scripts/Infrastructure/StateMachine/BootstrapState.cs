using Infrastructure.Factory.EnemyFactory;
using Infrastructure.Factory.EnvironmentFactory;
using Infrastructure.Factory.PlayerFactory;
using Infrastructure.Factory.SpawnersFactory;
using Services;
using Services.Ads;
using Services.AssetsProvider;
using Services.Input;
using Services.PersistentProgress;
using Services.SaveLoad;
using Services.SceneLoader;
using Services.ServiceLocator;
using Services.StaticData;
using UI.Services.Factory;
using UI.Services.WindowsService;
using UnityEngine;
using Watchers.SaveLoadWatchers;

namespace Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private const string SceneName = "Bootstrap";

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public void Enter()
        {
            _sceneLoader.LoadScene(SceneName, EnterLoadLevel);
        }

        public void Exit()
        {
            
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<ISaveLoadInstancesWatcher>(new SaveLoadInstancesWatcher());
            RegisterAddressableAssetProvider();
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<ISaveLoadService>
            (new SaveLoadService(GetAsset<ISaveLoadInstancesWatcher>(),
                GetAsset<IPersistentProgressService>()));
            _services.RegisterSingle<IPlayerFactory>
            (new PlayerFactory(
                GetAsset<ISaveLoadInstancesWatcher>()));

            RegisterStaticDataService();
            RegisterAdsService();

            _services.RegisterSingle<IUIFactory>(new UIFactory(GetAsset<IAddressableAssetProvider>(), 
                GetAsset<IStaticDataService>(), 
                GetAsset<IPersistentProgressService>(), GetAsset<IAdsService>()));

            _services.RegisterSingle<IWindowService>(new WindowService(GetAsset<IUIFactory>()));


            _services.RegisterSingle<IEnvironmentFactory>
                (new EnvironmentFactory());

            _services.RegisterSingle<IEnemyFactory>
            (new EnemyFactory
            (GetAsset<IStaticDataService>(), 
                GetAsset<IPlayerFactory>(), 
                GetAsset<IEnvironmentFactory>(), 
                GetAsset<ISaveLoadInstancesWatcher>(),
                GetAsset<IPersistentProgressService>(),
                GetAsset<IAddressableAssetProvider>()));

            _services.RegisterSingle<ISpawnerFactory>
                (new SpawnerFactory(GetAsset<IAddressableAssetProvider>(), 
                    GetAsset<ISaveLoadInstancesWatcher>(),
                    GetAsset<IEnemyFactory>()));
            
            _services.RegisterSingle<IGameStateMachine>(_gameStateMachine);
        }

        private void RegisterAddressableAssetProvider()
        {
            var addressableAssetProvider = new AddressableAssetProvider();
            addressableAssetProvider.CleanUp();
            addressableAssetProvider.Initialize();

            _services.RegisterSingle<IAddressableAssetProvider>(addressableAssetProvider);
        }

        private void RegisterAdsService()
        {
            var adsService = new AdsService();
            adsService.Initialize();
            _services.RegisterSingle<IAdsService>(adsService);
        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.SwitchState<ProgressLoadingState>();
        }

        private void RegisterStaticDataService()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadStaticData();
            _services.RegisterSingle(staticDataService);
        }

        private T GetAsset<T>() where T : IService
        {
            return _services.Single<T>();
        }

        private IInputService InputService()
        {
            if (Application.isEditor)
            {
                return new StandaloneInputService();
            }
            
            return  new MobileInputService();
        }
    }
}