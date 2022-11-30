namespace Infrastructure.StateMachine
{
    public interface IState : IBaseState
    {
        public void Enter();
    }
    
    public interface IStateWithOneArg<T0> : IBaseState
    {
        public void Enter(T0 arg);
    }

    public interface IBaseState
    {
        public void Exit();
    }
}