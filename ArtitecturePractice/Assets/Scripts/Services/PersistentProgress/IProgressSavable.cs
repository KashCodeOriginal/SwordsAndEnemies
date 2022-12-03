using Data.Player;

namespace Services.PersistentProgress
{
    public interface IProgressLoadable
    {
        void LoadProgress(PlayerProgress playerProgress);
    }

    public interface IProgressSavable
    {
        public void UpdateProgress(PlayerProgress playerProgress);
    }
}