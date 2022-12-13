using Services;
using UnityEngine;

namespace Infrastructure.Factory.PlayerFactory
{
    public interface IPlayerFactory : IPlayerFactoryInfo, IService
    {
        public GameObject CreatePlayer(Vector3 position);
        public void DestroyPlayer();
    }
}