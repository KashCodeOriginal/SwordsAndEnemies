namespace Enemy.Animation
{
    public interface IAnimationStateReader
    {
        AnimatorState State { get; }
        public void EnteredState(int stateHash);
        public void ExitedState(int stateHash);
    }
}