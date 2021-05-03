#region

using UnityEditor;
using UnityEngine;

#endregion

namespace Appalachia.Editor.Preferences.API
{
    internal struct Vector4_EPAPI : IEditorPreferenceAPI<Vector4>
    {
        public Vector4 Get(string key, Vector4 defaultValue, Vector4 low, Vector4 high)
        {
            var result = Vector4.zero;
            result.x = EditorPrefs.GetFloat($"{key}.x", defaultValue.x);
            result.y = EditorPrefs.GetFloat($"{key}.y", defaultValue.y);
            result.z = EditorPrefs.GetFloat($"{key}.z", defaultValue.z);
            result.w = EditorPrefs.GetFloat($"{key}.w", defaultValue.w);
            return result;
        }

        public void Save(string key, Vector4 value, Vector4 low, Vector4 high)
        {
            EditorPrefs.SetFloat($"{key}.x", value.x);
            EditorPrefs.SetFloat($"{key}.y", value.y);
            EditorPrefs.SetFloat($"{key}.z", value.z);
            EditorPrefs.SetFloat($"{key}.w", value.w);
        }

        public Vector4 Draw(string label, Vector4 value, Vector4 low, Vector4 high)
        {
            return EditorGUILayout.Vector4Field(label, value);
        }
    }
}
