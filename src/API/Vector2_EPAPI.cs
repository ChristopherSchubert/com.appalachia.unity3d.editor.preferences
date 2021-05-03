#region

using UnityEditor;
using UnityEngine;

#endregion

namespace Appalachia.Editor.Preferences.API
{
    internal struct Vector2_EPAPI : IEditorPreferenceAPI<Vector2>
    {
        public Vector2 Get(string key, Vector2 defaultValue, Vector2 low, Vector2 high)
        {
            var result = Vector2.zero;
            result.x = EditorPrefs.GetFloat($"{key}.x", defaultValue.x);
            result.y = EditorPrefs.GetFloat($"{key}.y", defaultValue.y);
            return result;
        }

        public void Save(string key, Vector2 value, Vector2 low, Vector2 high)
        {
            EditorPrefs.SetFloat($"{key}.x", value.x);
            EditorPrefs.SetFloat($"{key}.y", value.y);
        }

        public Vector2 Draw(string label, Vector2 value, Vector2 low, Vector2 high)
        {
            return EditorGUILayout.Vector2Field(label, value);
        }
    }
}
