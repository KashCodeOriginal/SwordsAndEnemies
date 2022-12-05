using Hero;
using UnityEngine;
using CameraControl;
using Data.Assets;
using UI.LoadingScreen;
using Services.SceneLoader;
using Infrastructure.Factory.PlayerFactory;
using Infrastructure.Factory.EnvironmentFactory;
using Services.PersistentProgress;
using Watchers.SaveLoadWatchers;

namespace Infrastructure.StateMachine
{
    public class LevelLoadingState : IStateWithOneArg<string>
    {
        public LevelLoadingState(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
            LoadingScreen loadingScreen, IPlayerFactory playerFactory, IEnvironmentFactory environmentFactory, ISaveLoadInstancesWatcher saveLoadInstancesWatcher, IPersistentProgressService persistentProgressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _playerFactory = playerFactory;
            _environmentFactory = environmentFactory;
            _saveLoadInstancesWatcher = saveLoadInstancesWatcher;
            _persistentProgressService = persistentProgressService;
        }

        private readonly GameStateMachine _gameStateMachine;

        private readonly SceneLoader _sceneLoader;

        private readonly LoadingScreen _loadingScreen;

        private readonly IPlayerFactory _playerFactory;

        private readonly IEnvironmentFactory _environmentFactory;

        private readonly ISaveLoadInstancesWatcher _saveLoadInstancesWatcher;
        private readonly IPersistentProgressService _persistentProgressService;

        public void Enter(string sceneName)
        {
            _loadingScreen.Show();

            _sceneLoader.LoadScene(sceneName, OnSceneLoaded); 
        }

        public void Exit()
        {
            _loadingScreen.Hide();
        } 

        private void OnSceneLoaded()
        {
            InitGameWorld();

            InformProgressReaders();
            
            _gameStateMachine.SwitchState<GamePlayState>();
        }

        private void InformProgressReaders()
        {
            foreach (var progressLoadable in _saveLoadInstancesWatcher.ProgressLoadableInstances)
            {
                progressLoadable.LoadProgress(_persistentProgressService.PlayerProgress);
            }
        }

        private void InitGameWorld()
        {
            var hero = _playerFactory.CreatePlayer();

            var gameplayScreen = _environmentFactory.CreateInstance(AssetsConstants.GAMEPLAY_SCREEN_PREFAB_PATH);
            var camera = _environmentFactory.CreateInstance(AssetsConstants.CAMERA_PREFAB_PATH);

            CameraFollow(camera, hero);
            SetUp(hero, camera);
        }


        private void SetUp(GameObject hero, GameObject cam)
        {
            if (hero.TryGetComponent(out HeroMovement heroMovement))
            {
                heroMovement.SetUp(cam.GetComponent<Camera>());
            }
        }

        private void CameraFollow(GameObject camera, GameObject hero)
        {
            if (camera.TryGetComponent(out CameraFollow cameraFollow))
            {
                cameraFollow.SetFollowingTarget(hero);
            }
        }
    }
}