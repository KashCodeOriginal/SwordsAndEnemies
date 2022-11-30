using System;
using System.Collections.Generic;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type,IBaseState> _states;

        private IBaseState _currentState;

        public GameStateMachine(SceneLoader sceneLoader)
        {
            _states = new Dictionary<Type, IBaseState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
                [typeof(LevelLoadingState)] = new LevelLoadingState(this, sceneLoader)
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
