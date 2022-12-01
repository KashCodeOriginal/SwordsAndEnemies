using UnityEngine;

namespace Infrastructure.Factory.PlayerFactory
{
    public interface IPlayerFactoryInfo
    {
        public GameObject PlayerInstance { get; }
    }
}