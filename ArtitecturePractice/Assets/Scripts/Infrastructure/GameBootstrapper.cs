using UnityEngine;
using Infrastructure.StateMachine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _gameInstance;

        private void Awake()
        {
            _gameInstance = new Game(this);
            
            _gameInstance.StateMachine.SwitchState<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}

