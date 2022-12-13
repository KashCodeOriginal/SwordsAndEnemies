using System.Linq;
using Services.StaticData;
using Spawners;
using Spawners.Enemy;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        private const string InitialPlayerPoint = "PlayerInitialPoint";
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                levelData.EnemySpawners = FindObjectsOfType<SpawnMarker>().Select(x =>
                        new EnemySpawnerData(x.GetComponent<UniqueID>().Id, x.MonsterTypeId, x.transform.position)).ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;

                levelData.InitialPlayerPosition = GameObject.FindWithTag(InitialPlayerPoint).transform.position;
            }
            
            EditorUtility.SetDirty(target);
        }
    }
}