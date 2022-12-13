using UnityEngine;
using CameraControl;
using Data.Assets;
using Infrastructure.Factory.EnemyFactory;
using UI.LoadingScreen;
using Services.SceneLoader;
using Infrastructure.Factory.PlayerFactory;
using Infrastructure.Factory.EnvironmentFactory;
using Infrastructure.Factory.SpawnersFactory;
using Services.Input;
using Services.PersistentProgress;
using Services.StaticData;
using UI.GameplayScreen;
using UI.Services.Factory;
using UI.Services.WindowsService;
using UI.ShopUI;
using Units.Hero;
using UnityEngine.SceneManagement;
using Watchers.SaveLoadWatchers;

namespace Infrastructure.StateMachine
{
    public class LevelLoadingState : IStateWithOneArg<string>
    {
        public LevelLoadingState(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
            LoadingScreen loadingScreen, IPlayerFactory playerFactory, IEnvironmentFactory environmentFactory,
            ISaveLoadInstancesWatcher saveLoadInstancesWatcher, IPersistentProgressService persistentProgressService,
            IInputService inputService, IStaticDataService staticDataService, ISpawnerFactory spawnerFactory, IWindowService windowService, IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _playerFactory = playerFactory;
            _environmentFactory = environmentFactory;
            _saveLoadInstancesWatcher = saveLoadInstancesWatcher;
            _persistentProgressService = persistentProgressService;
            _inputService = inputService;
            _staticDataService = staticDataService;
            _spawnerFactory = spawnerFactory;
            _windowService = windowService;
            _uiFactory = uiFactory;
        }


        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;
        private readonly IPlayerFactory _playerFactory;
        private readonly IEnvironmentFactory _environmentFactory;
        private readonly ISaveLoadInstancesWatcher _saveLoadInstancesWatcher;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IInputService _inputService;
        private readonly IStaticDataService _staticDataService;
        private readonly ISpawnerFactory _spawnerFactory;
        private readonly IWindowService _windowService;
        private readonly IUIFactory _uiFactory;

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
            var levelData = GetLevelStaticData();

            InitUIRoot();
            
            InitSpawners(levelData);
            
            var hero = _playerFactory.CreatePlayer(levelData.InitialPlayerPosition);

            var gameplayScreen = _environmentFactory.CreateInstance(AssetsConstants.GAMEPLAY_SCREEN_PREFAB_PATH);
            var camera = _environmentFactory.CreateInstance(AssetsConstants.CAMERA_PREFAB_PATH);

            CameraFollow(camera, hero);
            SetUp(hero, camera, gameplayScreen);
        }

        private LevelStaticData GetLevelStaticData()
        {
            var sceneKey = SceneManager.GetActiveScene().name;
            var levelData = _staticDataService.ForLevel(sceneKey);
            return levelData;
        }

        private void InitUIRoot()
        {
            _uiFactory.CreateUIRoot();
        }

        private void InitSpawners(LevelStaticData levelData)
        {
            foreach (var data in levelData.EnemySpawners)
            {
                _spawnerFactory.CreateSpawner(data.Position, data.ID, data.MonsterTypeID);
            }
        }

        private void SetUp(GameObject hero, GameObject cam, GameObject gameplayScreen)
        {
            if (hero.TryGetComponent(out HeroMovement heroMovement))
            {
                heroMovement.SetUp(cam.GetComponent<Camera>());
            }

            if (hero.TryGetComponent(out HeroAttack heroAttack))
            {
                heroAttack.SetUp(_inputService);
            }

            if (gameplayScreen.TryGetComponent(out ActorUI actorUI))
            {
                actorUI.SetUp(hero.GetComponent<HeroHealth>());
            }

            gameplayScreen.GetComponentInChildren<LootCounter>().Construct(_persistentProgressService.PlayerProgress.WorldData);

            foreach (var button in gameplayScreen.GetComponentsInChildren<OpenWindowButton>())
            {
                button.Construct(_windowService);   
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