using System.Threading.Tasks;
using Services;
using UnityEngine;

namespace Infrastructure.Factory.PlayerFactory
{
    public interface IPlayerFactory : IPlayerFactoryInfo, IService
    {
        public GameObject CreatePlayer(GameObject prefab, Vector3 position);
        public void DestroyPlayer();
    }
}