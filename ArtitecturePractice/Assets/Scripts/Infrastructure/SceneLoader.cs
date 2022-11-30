using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader
    {
        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        private readonly ICoroutineRunner _coroutineRunner;

        public void LoadScene(string sceneName, Action onSceneLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadSceneAsync(sceneName, onSceneLoaded));
        }
        
        private IEnumerator LoadSceneAsync(string sceneName, Action onSceneLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                onSceneLoaded?.Invoke();
                yield break;
            }
            
            var loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);

            while (!loadSceneAsync.isDone)
            {
                yield return null;
            }
            
            onSceneLoaded?.Invoke();
        }
    }
}