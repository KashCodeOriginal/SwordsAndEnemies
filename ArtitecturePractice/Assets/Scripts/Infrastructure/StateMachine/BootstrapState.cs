using Services.Input;
using UnityEngine;

namespace Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private const string SceneName = "Bootstrap";

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public void Enter()
        {
            RegisterServices();
            
            _sceneLoader.LoadScene(SceneName, EnterLoadLevel);
        }

        public void Exit()
        {
            
        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.SwitchState<LevelLoadingState, string>("MainLevel");
        }

        private void RegisterServices()
        {
            Game.InputService = RegisterInputService();
        }
        
        private IInputService RegisterInputService()
        {
            if (Application.isEditor)
            {
                return new StandaloneInputService();
            }
            
            return  new MobileInputService();
        }
    }
}