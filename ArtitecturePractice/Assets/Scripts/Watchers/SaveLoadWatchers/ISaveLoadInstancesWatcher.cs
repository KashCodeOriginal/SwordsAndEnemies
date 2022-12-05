using System.Collections.Generic;
using Services;
using Services.PersistentProgress;
using UnityEngine;

namespace Watchers.SaveLoadWatchers
{
    public interface ISaveLoadInstancesWatcher : IService
    {
        public IReadOnlyList<IProgressSavable> ProgressSavableInstances { get; }
        public IReadOnlyList<IProgressLoadable> ProgressLoadableInstances { get; }
        
        public void RegisterProgress(GameObject instance);
        public void CleanAll();
    }
}