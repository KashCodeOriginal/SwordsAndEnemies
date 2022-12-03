using System;
using UI.LoadingScreen;
using Services.SceneLoader;
using System.Collections.Generic;
using Infrastructure.Factory.EnvironmentFactory;
using Infrastructure.Factory.PlayerFactory;
using Services.PersistentProgress;
using Services.SaveLoadService;
using Services.ServiceLocator;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type,IBaseState> _states;

        private IBaseState _currentState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingScreen loadingScreen, AllServices services)
        {
            _states = new Dictionary<Type, IBaseState>()
            {
                [typeof(GamePlayState)] = new GamePlayState(this),
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(ProgressLoadingState)] = new ProgressLoadingState(this, services.Single<IPersistentProgressService>(), services.Single<ISaveLoadService>()),
                [typeof(LevelLoadingState)] = new LevelLoadingState(this, sceneLoader, loadingScreen, services.Single<IPlayerFactory>(), services.Single<IEnvironmentFactory>())
            }; 
        }
        
        public void SwitchState<TState>() where TState : class, IState
        {
            var state = SetNewState<TState>();

            state?.Enter();
        }

        public void SwitchState<TState, T0>(T0 arg) where TState : class, IStateWithOneArg<T0>
        {
            var state = SetNewState<TState>();
            
            state?.Enter(arg);
        }

        private TState SetNewState<TState>() where TState : class, IBaseState
        {
            _currentState?.Exit();

            var state = GetNewState<TState>();

            _currentState = state;
            
            return state;
        }

        private TState GetNewState<TState>() where TState : class, IBaseState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}
