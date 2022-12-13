using Services;

namespace Infrastructure.StateMachine
{
    public interface IGameStateMachine : IService
    {
        void SwitchState<TState>() where TState : class, IState;
        void SwitchState<TState, T0>(T0 arg) where TState : class, IStateWithOneArg<T0>;
    }
}