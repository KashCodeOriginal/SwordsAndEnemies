using Services;
using UnityEngine;

namespace Infrastructure.Factory.PlayerFactory
{
    public interface IPlayerFactory : IPlayerFactoryInfo, IService
    {
        public GameObject CreatePlayer();
        public void DestroyPlayer();
    }
}