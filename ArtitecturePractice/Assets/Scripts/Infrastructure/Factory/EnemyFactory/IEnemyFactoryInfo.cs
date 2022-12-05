using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Factory.EnemyFactory
{
    public interface IEnemyFactoryInfo
    {
        public IReadOnlyList<GameObject> Instances { get; }
    }
}
