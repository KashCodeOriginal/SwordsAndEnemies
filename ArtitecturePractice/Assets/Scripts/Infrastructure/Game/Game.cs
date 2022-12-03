using Infrastructure.StateMachine;
using Services.CoroutineRunner;
using Services.SceneLoader;
using Services.ServiceLocator;
using UI.LoadingScreen;

namespace Infrastructure.Game
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