using Infrastructure.Factory.EnemyFactory;
using Infrastructure.Factory.EnvironmentFactory;
using Infrastructure.Factory.PlayerFactory;
using Infrastructure.Factory.SpawnersFactory;
using Services;
using Services.AssetsProvider;
using Services.Input;
using Services.PersistentProgress;
using Services.SaveLoadService;
using Services.SceneLoader;
using Services.ServiceLocator;
using Services.StaticData;
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
            _services.RegisterSingle<IAssetsProvider>(new AssetsProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<ISaveLoadService>
            (new SaveLoadService(GetAsset<ISaveLoadInstancesWatcher>(),
                GetAsset<IPersistentProgressService>()));
            _services.RegisterSingle<IPlayerFactory>
            (new PlayerFactory(GetAsset<IAssetsProvider>(),
                GetAsset<ISaveLoadInstancesWatcher>()));

            RegisterStaticDataService();


            _services.RegisterSingle<IEnvironmentFactory>
                (new EnvironmentFactory(GetAsset<IAssetsProvider>()));
            
            _services.RegisterSingle<IEnemyFactory>
            (new EnemyFactory
            (GetAsset<IStaticDataService>(), 
                GetAsset<IPlayerFactory>(), 
                GetAsset<IEnvironmentFactory>(), 
                GetAsset<ISaveLoadInstancesWatcher>(),
                GetAsset<IPersistentProgressService>()));


            _services.RegisterSingle<ISpawnerFactory>
                (new SpawnerFactory(GetAsset<IAssetsProvider>(), GetAsset<ISaveLoadInstancesWatcher>(), GetAsset<IEnemyFactory>()));
        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.SwitchState<ProgressLoadingState>();
        }

        private void RegisterStaticDataService()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadMonsters();
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