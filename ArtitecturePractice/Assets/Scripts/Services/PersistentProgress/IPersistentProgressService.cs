using Data.Player;

namespace Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress PlayerProgress { get; }
        public void SetProgress(PlayerProgress playerProgress);
    }
}