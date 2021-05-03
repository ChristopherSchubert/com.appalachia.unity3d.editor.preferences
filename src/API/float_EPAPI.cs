#region

using UnityEditor;
using UnityEngine;

#endregion

namespace Appalachia.Editor.Preferences.API
{
    internal struct float_EPAPI : IEditorPreferenceAPI<float>
    {
        public float Get(string key, float defaultValue, float low, float high)
        {
            var val = EditorPrefs.GetFloat(key, defaultValue);
            return low == high ? val : Mathf.Clamp(val, low, high);
        }

        public void Save(string key, float value, float low, float high)
        {
            EditorPrefs.SetFloat(key, low == high ? value : Mathf.Clamp(value, low, high));
        }

        public float Draw(string label, float value, float low, float high)
        {
            var val = low != high
                ? EditorGUILayout.Slider(label, value, low, high)
                : EditorGUILayout.FloatField(label, value);

            return val;
        }
    }
}
