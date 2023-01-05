namespace Units.Enemy.Animation
{
    public interface IAnimationStateReader
    {
        public AnimatorState State { get; }
        public void EnteredState(int stateHash);
        public void ExitedState(int stateHash);
    }
}