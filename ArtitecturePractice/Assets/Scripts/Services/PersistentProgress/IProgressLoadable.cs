using Data.Player;

namespace Services.PersistentProgress
{
    public interface IProgressLoadable
    {
        void LoadProgress(PlayerProgress playerProgress);
    }
}