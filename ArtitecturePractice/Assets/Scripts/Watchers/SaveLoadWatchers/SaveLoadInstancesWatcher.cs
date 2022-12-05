using System.Collections.Generic;
using Services.PersistentProgress;
using UnityEngine;

namespace Watchers.SaveLoadWatchers
{
    public class SaveLoadInstancesWatcher : ISaveLoadInstancesWatcher
    {
        private List<IProgressSavable> _progressSavableInstances = new List<IProgressSavable>();
        private List<IProgressLoadable> _progressLoadableInstances = new List<IProgressLoadable>();
        
        public IReadOnlyList<IProgressSavable> ProgressSavableInstances => _progressSavableInstances;
        public IReadOnlyList<IProgressLoadable> ProgressLoadableInstances => _progressLoadableInstances;

        public void RegisterProgress(GameObject instance)
        {
            foreach (var progressReader in instance.GetComponentsInChildren<IProgressLoadable>())
            {
                Register(progressReader);
            }
        }

        public void CleanAll()
        {
            _progressLoadableInstances.Clear();
            _progressSavableInstances.Clear();
        }

        private void Register(IProgressLoadable progressLoadable)
        {
            if (progressLoadable is IProgressSavable progressSavable)
            {
                _progressSavableInstances.Add(progressSavable);
            }
            
            _progressLoadableInstances.Add(progressLoadable);
        }
    }
}