using System.Threading.Tasks;
using UnityEngine;
using CameraControl;
using Data.Assets;
using UI.LoadingScreen;
using Services.SceneLoader;
using Infrastructure.Factory.PlayerFactory;
using Infrastructure.Factory.EnvironmentFactory;
using Infrastructure.Factory.SpawnersFactory;
using Services.AssetsProvider;
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
            IInputService inputService, IStaticDataService staticDataService, ISpawnerFactory spawnerFactory,
            IWindowService windowService, IUIFactory uiFactory, IAddressableAssetProvider addressableAssetProvider)
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
            _addressableAssetProvider = addressableAssetProvider;
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
        private readonly IAddressableAssetProvider _addressableAssetProvider;

        public void Enter(string sceneName)
        {
            _loadingScreen.Show();

            _sceneLoader.LoadScene(sceneName, OnSceneLoaded); 
        }

        public void Exit()
        {
            _loadingScreen.Hide();
        }

        private async void OnSceneLoaded()
        {
            await InitGameWorld();

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

        private async Task InitGameWorld()
        {
            var levelData = GetLevelStaticData();

            InitUIRoot();
            
            await InitSpawners(levelData);

            var heroPrefab = await 
                _addressableAssetProvider.GetAsset<GameObject>(AssetsAddressablesConstants.PLAYER_PREFAB_PATH);
            var gameplayScreenPrefab = await
                _addressableAssetProvider.GetAsset<GameObject>(AssetsAddressablesConstants.GAMEPLAY_SCREEN_PREFAB_PATH);
            var cameraPrefab = await
                _addressableAssetProvider.GetAsset<GameObject>(AssetsAddressablesConstants.CAMERA_PREFAB_PATH);

            var hero = _playerFactory.CreatePlayer(heroPrefab, levelData.InitialPlayerPosition);
            var gameplayScreen = _environmentFactory.CreateInstance(gameplayScreenPrefab);
            var camera = _environmentFactory.CreateInstance(cameraPrefab);

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

        private Task InitSpawners(LevelStaticData levelData)
        {
            foreach (var data in levelData.EnemySpawners)
            {
                _spawnerFactory.CreateSpawner(data.Position, data.ID, data.MonsterTypeID);
            }
            
            return Task.CompletedTask;
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