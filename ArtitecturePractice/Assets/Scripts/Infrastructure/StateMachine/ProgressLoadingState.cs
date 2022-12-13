using Data.Player;
using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;

namespace Infrastructure.StateMachine
{
    public class ProgressLoadingState : IState
    {
        public ProgressLoadingState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public void Enter()
        {
            LoadProgressOrInitNew();

            var worldData = _progressService.PlayerProgress.WorldData.PositionOnLevel;
            
            _gameStateMachine.SwitchState<LevelLoadingState, string>(worldData.LevelName);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.SetProgress(_saveLoadService.LoadProgress() ?? NewProgress());
        }

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress("MainLevel")
            {
                HeroState =
                {
                    MaxHP = 100 
                },
                HeroStats =
                {
                    Damage = 5,
                    DamageRadius = 0.5f
                }
            };

            progress.HeroState.ResetHP();

            return progress;
        }
    }
}