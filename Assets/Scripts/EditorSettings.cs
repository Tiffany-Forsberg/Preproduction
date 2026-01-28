using UnityEngine;

[CreateAssetMenu(fileName = "EditorSettings", menuName = "Scriptable Objects/EditorSettings")]
public class EditorSettings : ScriptableObject
{
    public CustomEditorObject[] Objects;
}
