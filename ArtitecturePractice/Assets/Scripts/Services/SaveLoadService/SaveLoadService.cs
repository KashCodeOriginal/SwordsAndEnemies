using Data.Player;
using Infrastructure.StateMachine;
using UnityEngine;

namespace Services.SaveLoadService
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "ProgressKey";

        public void SaveProgress()
        {
        }

        public PlayerProgress LoadProgress()
        {
           return PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
        }
    }
}