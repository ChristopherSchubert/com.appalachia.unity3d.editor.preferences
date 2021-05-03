#region

using UnityEditor;
using UnityEngine;

#endregion

namespace Appalachia.Editor.Preferences.API
{
    internal struct int_EPAPI : IEditorPreferenceAPI<int>
    {
        public int Get(string key, int defaultValue, int low, int high)
        {
            var val = EditorPrefs.GetInt(key, defaultValue);
            return low == high ? val : Mathf.Clamp(val, low, high);
        }

        public void Save(string key, int value, int low, int high)
        {
            EditorPrefs.SetInt(key, low == high ? value : Mathf.Clamp(value, low, high));
        }

        public int Draw(string label, int value, int low, int high)
        {
            var val = low != high
                ? EditorGUILayout.IntSlider(label, value, low, high)
                : EditorGUILayout.IntField(label, value);

            return val;
        }
    }
}
