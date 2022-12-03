using Infrastructure.Factory.EnvironmentFactory;
using Infrastructure.Factory.PlayerFactory;
using Services.AssetsProvider;
using Services.Input;
using Services.PersistentProgress;
using Services.SaveLoadService;
using Services.SceneLoader;
using Services.ServiceLocator;
using UnityEngine;

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

        private void EnterLoadLevel()
        {
            _gameStateMachine.SwitchState<ProgressLoadingState>();
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<IAssetsProvider>(new AssetsProvider());
            _services.RegisterSingle<IPlayerFactory>(new PlayerFactory(_services.Single<IAssetsProvider>()));
            _services.RegisterSingle<IEnvironmentFactory>(new EnvironmentFactory(_services.Single<IAssetsProvider>()));
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService());
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