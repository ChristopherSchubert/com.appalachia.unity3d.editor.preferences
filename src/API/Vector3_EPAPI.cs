#region

using UnityEditor;
using UnityEngine;

#endregion

namespace Appalachia.Editor.Preferences.API
{
    internal struct Vector3_EPAPI : IEditorPreferenceAPI<Vector3>
    {
        public Vector3 Get(string key, Vector3 defaultValue, Vector3 low, Vector3 high)
        {
            var result = Vector3.zero;
            result.x = EditorPrefs.GetFloat($"{key}.x", defaultValue.x);
            result.y = EditorPrefs.GetFloat($"{key}.y", defaultValue.y);
            result.z = EditorPrefs.GetFloat($"{key}.z", defaultValue.z);
            return result;
        }

        public void Save(string key, Vector3 value, Vector3 low, Vector3 high)
        {
            EditorPrefs.SetFloat($"{key}.x", value.x);
            EditorPrefs.SetFloat($"{key}.y", value.y);
            EditorPrefs.SetFloat($"{key}.z", value.z);
        }

        public Vector3 Draw(string label, Vector3 value, Vector3 low, Vector3 high)
        {
            return EditorGUILayout.Vector3Field(label, value);
        }
    }
}
