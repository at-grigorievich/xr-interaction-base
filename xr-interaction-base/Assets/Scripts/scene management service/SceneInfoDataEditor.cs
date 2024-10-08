using UnityEngine;
using UnityEditor;

namespace ATG.SceneManagement
{
#if UNITY_EDITOR
    [CustomEditor(typeof(SceneInfoData))]
    public class SceneInfoDataEditor: Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var config = target as SceneInfoData;

            if(GUILayout.Button("Check correct", GUILayout.Height(40)))
            {
                bool hasScene = config.IsSceneHasInBuildSettings(out int index);

                if(hasScene == true)
                {
                    Debug.Log("<color=green>scene is correct installed!</color>");
                }
                else
                {
                    Debug.LogWarning($"Add scene {config.Scene.name} to build settings !!");
                }
            }
        }
    }
#endif
}