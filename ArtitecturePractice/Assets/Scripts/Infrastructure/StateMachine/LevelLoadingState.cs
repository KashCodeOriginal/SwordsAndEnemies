using Hero;
using UnityEngine;
using CameraControl;
using Data.Assets;
using Infrastructure.Factory.EnemyFactory;
using UI.LoadingScreen;
using Services.SceneLoader;
using Infrastructure.Factory.PlayerFactory;
using Infrastructure.Factory.EnvironmentFactory;
using Services.Input;
using Services.PersistentProgress;
using Spawners;
using UI.GameplayScreen;
using Units.Enemy.Logic;
using Units.Hero;
using Watchers.SaveLoadWatchers;

namespace Infrastructure.StateMachine
{
    public class LevelLoadingState : IStateWithOneArg<string>
    {
        public LevelLoadingState(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
            LoadingScreen loadingScreen, IPlayerFactory playerFactory, IEnvironmentFactory environmentFactory,
            ISaveLoadInstancesWatcher saveLoadInstancesWatcher, IPersistentProgressService persistentProgressService,
            IEnemyFactory enemyFactory, IInputService inputService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _playerFactory = playerFactory;
            _environmentFactory = environmentFactory;
            _saveLoadInstancesWatcher = saveLoadInstancesWatcher;
            _persistentProgressService = persistentProgressService;
            _enemyFactory = enemyFactory;
            _inputService = inputService;
        }


        private readonly GameStateMachine _gameStateMachine;

        private readonly SceneLoader _sceneLoader;

        private readonly LoadingScreen _loadingScreen;

        private readonly IPlayerFactory _playerFactory;

        private readonly IEnvironmentFactory _environmentFactory;

        private readonly ISaveLoadInstancesWatcher _saveLoadInstancesWatcher;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IInputService _inputService;

        private const string SpawnerTag = "Spawner";

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
            InitSpawners();
            
            var hero = _playerFactory.CreatePlayer();

            var gameplayScreen = _environmentFactory.CreateInstance(AssetsConstants.GAMEPLAY_SCREEN_PREFAB_PATH);
            var camera = _environmentFactory.CreateInstance(AssetsConstants.CAMERA_PREFAB_PATH);

            var enemy = _enemyFactory.CreateInstance(AssetsConstants.ENEMY_PREFAB_PATH);

            CameraFollow(camera, hero);
            SetUp(hero, camera, enemy, gameplayScreen);
        }

        private void InitSpawners()
        {
            var spawners = GameObject.FindGameObjectsWithTag(SpawnerTag);

            foreach (var spawner in spawners)
            {
                _saveLoadInstancesWatcher.RegisterProgress(spawner);
            }
        }

        private void SetUp(GameObject hero, GameObject cam, GameObject enemy, GameObject gameplayScreen)
        {
            if (hero.TryGetComponent(out HeroMovement heroMovement))
            {
                heroMovement.SetUp(cam.GetComponent<Camera>());
            }

            if (hero.TryGetComponent(out HeroAttack heroAttack))
            {
                heroAttack.SetUp(_inputService);
            }

            if (enemy.TryGetComponent(out MoveToPlayer moveToPlayer))
            {
                moveToPlayer.SetTarget(hero.transform);
            }
            
            if (enemy.TryGetComponent(out RotateToPlayer rotateToPlayer))
            {
                rotateToPlayer.SetPlayer(hero.transform);
            }

            if (enemy.TryGetComponent(out EnemyAttack attack))
            {
                attack.SetUpPlayer(hero.transform);
            }

            if (gameplayScreen.TryGetComponent(out ActorUI actorUI))
            {
                actorUI.SetUp(hero.GetComponent<HeroHealth>());
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