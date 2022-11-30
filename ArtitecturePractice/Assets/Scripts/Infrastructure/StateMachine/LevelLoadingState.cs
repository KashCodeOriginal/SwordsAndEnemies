using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.StateMachine
{
    public class LevelLoadingState : IStateWithOneArg<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public LevelLoadingState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.LoadScene(sceneName, OnSceneLoaded); 
        }

        public void Exit()
        {
        }

        private void OnSceneLoaded()
        {
            var hero = Instantiate("Data/Prefabs/Player/Player");
            var gameplayScreen = Instantiate("Data/Prefabs/UI/GameplayScreen");
            
            
        }

        private GameObject Instantiate(string path)
        {
            var heroPrefab = Resources.Load<GameObject>(path);

            var heroInstance = Object.Instantiate(heroPrefab);

            return heroInstance;
        }
    }
}