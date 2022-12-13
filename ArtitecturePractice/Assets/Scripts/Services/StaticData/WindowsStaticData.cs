using System.Collections.Generic;
using UnityEngine;

namespace Services.StaticData
{
    [CreateAssetMenu(fileName = "WindowStaticData", menuName = "StaticData/Windows")]
    public class WindowsStaticData : ScriptableObject
    {
        [SerializeField] private List<WindowConfig> _windowConfigs = new List<WindowConfig>();
        
        public List<WindowConfig> WindowConfigs => _windowConfigs;
    }
}