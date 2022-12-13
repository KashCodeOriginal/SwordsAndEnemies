using System;
using System.Linq;
using Spawners;
using Spawners.Enemy;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(UniqueID))]
    public class UniqueIdEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            UniqueID uniqueID = (UniqueID) target;

            if (string.IsNullOrEmpty(uniqueID.Id))
            {
                Generate(uniqueID);
            }
            else
            {
                var ids = FindObjectsOfType<UniqueID>();
                
                if(ids.Any(other => other != uniqueID && other.Id == uniqueID.Id))
                {
                    Generate(uniqueID);
                }
            }
        }

        private void Generate(UniqueID uniqueID)
        {
            uniqueID.Id = $"{uniqueID.gameObject.scene.name}_{Guid.NewGuid()}";

            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(uniqueID);
                EditorSceneManager.MarkSceneDirty(uniqueID.gameObject.scene);
            }
        }
    }
}