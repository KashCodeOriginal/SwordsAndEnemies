using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Factory.EnvironmentFactory
{
    public interface IEnvironmentFactoryInfo
    {
        public IReadOnlyList<GameObject> Instances { get; }
    }
}