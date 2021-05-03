#region

using UnityEditor;
using UnityEngine;

#endregion

namespace Appalachia.Editor.Preferences.API
{
    internal struct Quaternion_EPAPI : IEditorPreferenceAPI<Quaternion>
    {
        public Quaternion Get(string key, Quaternion defaultValue, Quaternion low, Quaternion high)
        {
            var result = Quaternion.identity;
            var value = result;
            value.x = EditorPrefs.GetFloat($"{key}.x", defaultValue.x);
            value.y = EditorPrefs.GetFloat($"{key}.y", defaultValue.y);
            value.z = EditorPrefs.GetFloat($"{key}.z", defaultValue.z);
            value.w = EditorPrefs.GetFloat($"{key}.w", defaultValue.w);
            result = value;
            return result;
        }

        public void Save(string key, Quaternion value, Quaternion low, Quaternion high)
        {
            EditorPrefs.SetFloat($"{key}.x", value.x);
            EditorPrefs.SetFloat($"{key}.y", value.y);
            EditorPrefs.SetFloat($"{key}.z", value.z);
            EditorPrefs.SetFloat($"{key}.w", value.w);
        }

        public Quaternion Draw(string label, Quaternion value, Quaternion low, Quaternion high)
        {
            var euler = value.eulerAngles;
            euler = EditorGUILayout.Vector3Field(label, euler);

            return Quaternion.Euler(euler.x, euler.y, euler.z);
        }
    }
}
