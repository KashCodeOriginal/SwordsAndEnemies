using UnityEngine;

namespace Infrastructure.Game
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameObject _gameBootstrapperPrefab;
        
        private void Awake()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper == null)
            {
                 Instantiate(_gameBootstrapperPrefab);
            }
        }
    }
}