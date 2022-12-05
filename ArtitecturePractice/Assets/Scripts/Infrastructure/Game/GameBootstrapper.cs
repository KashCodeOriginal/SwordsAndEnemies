using Infrastructure.StateMachine;
using Services.CoroutineRunner;
using UI.LoadingScreen;
using UnityEngine;

namespace Infrastructure.Game
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingScreen _loadingScreenPrefab;
         
        private Game _gameInstance;

        private void Awake()
        {
            _gameInstance = new Game(this, Instantiate(_loadingScreenPrefab));
            
            _gameInstance.StateMachine.SwitchState<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}

