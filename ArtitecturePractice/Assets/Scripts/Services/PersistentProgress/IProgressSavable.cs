using Data.Player;

namespace Services.PersistentProgress
{
    public interface IProgressSavable : IProgressLoadable
    {
        public void UpdateProgress(PlayerProgress playerProgress);
    }
}