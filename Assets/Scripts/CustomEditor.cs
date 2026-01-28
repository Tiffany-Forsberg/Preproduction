using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using AYellowpaper.SerializedCollections;
using System.Numerics;
using System.Collections.Generic;

public class CustomEditor : EditorWindow
{
    private EditorSettings settings;
    private GameObject prefabToSpawn;
    
    private Dictionary<string, UnityEngine.Vector2> startPositions = new Dictionary<string, UnityEngine.Vector2>();

    // SerializedObject _objectSO;
    // ReorderableList _listRE;
    // private EditorObjectList _objectList;

    [MenuItem("Window/Level Editor")]
    public static void ShowWindow()
    {
        GetWindow<CustomEditor>("Level Editor");
    }

    void OnGUI()
    {
        if (startPositions.Count < settings.Objects.Length)
        {
            startPositions = new Dictionary<string, UnityEngine.Vector2>();
            foreach(var obj in settings.Objects)
            {
                startPositions.Add(obj.Name, new UnityEngine.Vector2(0, 0));
            }
        }

        GUILayout.Label("Settings", EditorStyles.boldLabel);

        settings = EditorGUILayout.ObjectField("EditorSettings", settings,
            typeof(EditorSettings), false) as EditorSettings;

        if (!settings)
        {
            UnityEngine.Debug.LogWarning("Add settings");
            return;
        }

        foreach (var obj in settings.Objects)
        {
            EditorGUILayout.LabelField($"Spawn {obj.Name} settings", EditorStyles.boldLabel);
            startPositions[obj.Name] = EditorGUILayout.Vector2Field("Spawn at position", startPositions[obj.Name]);
            if (GUILayout.Button($"Add instance of {obj.Name}"))
            {
                SpawnPrefab(obj.Prefab, startPositions[obj.Name]);  
            }
        }
        
    }

    void SpawnPrefab(GameObject prefab, UnityEngine.Vector2 position)
    {
        if (prefab == null)
        {
            return;
        }

        GameObject spawnedPrefab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        spawnedPrefab.transform.position = position;

        Selection.activeObject = spawnedPrefab;

        Undo.RegisterCreatedObjectUndo(spawnedPrefab, "Spawn prefab");
    }
}
