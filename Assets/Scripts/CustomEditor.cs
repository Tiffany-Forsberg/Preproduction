using UnityEngine;
using UnityEditor;
using System.Diagnostics;

public class CustomEditor : EditorWindow
{
    private GameObject prefabToSpawn;

    [MenuItem("Window/Save me")]
    public static void ShowWindow()
    {
        GetWindow<CustomEditor>("Save me");
    }

    void OnGUI()
    {
        GUILayout.Label("I am horrified.", EditorStyles.boldLabel);
        
        prefabToSpawn = EditorGUILayout.ObjectField("Prefab", prefabToSpawn,
            typeof(GameObject), false) as GameObject;
        
        if (GUILayout.Button("Add instance of prefab"))
        {
            UnityEngine.Debug.Log("Yay!");
            SpawnPrefab();  
        }
    }

    void SpawnPrefab()
    {
        if (prefabToSpawn == null)
        {
            return;
        }

        GameObject spawnedPrefab = PrefabUtility.InstantiatePrefab(prefabToSpawn) as GameObject;
        Undo.RegisterCreatedObjectUndo(spawnedPrefab, "Spawn prefab");
    }
}
