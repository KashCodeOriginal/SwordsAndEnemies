namespace Infrastructure.StateMachine
{
    public class GamePlayState : IState
    {
        private readonly GameStateMachine _gameStateMachine;

        public GamePlayState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}