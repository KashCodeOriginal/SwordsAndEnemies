using Services.Input;
using UI.LoadingScreen;
using Services.SceneLoader;
using Services.CoroutineRunner;
using Infrastructure.StateMachine;
using Services.ServiceLocator;

namespace Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;
        public Game(ICoroutineRunner coroutineRunner, LoadingScreen loadingScreen)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingScreen, AllServices.Container);
        }
    }
}