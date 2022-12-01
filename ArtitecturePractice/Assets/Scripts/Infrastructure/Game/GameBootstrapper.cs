using UnityEngine;
using Infrastructure.StateMachine;
using Services.CoroutineRunner;
using UI.LoadingScreen;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingScreen _loadingScreen;
         
        private Game _gameInstance;

        private void Awake()
        {
            _gameInstance = new Game(this, _loadingScreen);
            
            _gameInstance.StateMachine.SwitchState<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}

