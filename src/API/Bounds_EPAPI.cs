#region

using UnityEditor;
using UnityEngine;

#endregion

namespace Appalachia.Editor.Preferences.API
{
    internal struct Bounds_EPAPI : IEditorPreferenceAPI<Bounds>
    {
        public Bounds Get(string key, Bounds defaultValue, Bounds low, Bounds high)
        {
            var result = default(Bounds);

            var center = result.center;
            var size = result.size;

            center.x = EditorPrefs.GetFloat($"{key}.center.x", defaultValue.center.x);
            center.y = EditorPrefs.GetFloat($"{key}.center.y", defaultValue.center.y);
            center.z = EditorPrefs.GetFloat($"{key}.center.z", defaultValue.center.z);
            size.x = EditorPrefs.GetFloat($"{key}.size.x",     defaultValue.size.x);
            size.y = EditorPrefs.GetFloat($"{key}.size.y",     defaultValue.size.y);
            size.z = EditorPrefs.GetFloat($"{key}.size.z",     defaultValue.size.z);

            result.center = center;
            result.size = size;
            return result;
        }

        public void Save(string key, Bounds value, Bounds low, Bounds high)
        {
            EditorPrefs.SetFloat($"{key}.center.x", value.center.x);
            EditorPrefs.SetFloat($"{key}.center.y", value.center.y);
            EditorPrefs.SetFloat($"{key}.center.z", value.center.z);
            EditorPrefs.SetFloat($"{key}.size.x",   value.size.x);
            EditorPrefs.SetFloat($"{key}.size.y",   value.size.y);
            EditorPrefs.SetFloat($"{key}.size.z",   value.size.z);
        }

        public Bounds Draw(string label, Bounds value, Bounds low, Bounds high)
        {
            return EditorGUILayout.BoundsField(label, value);
        }
    }
}
