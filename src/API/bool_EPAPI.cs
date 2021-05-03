#region

using UnityEditor;

#endregion

namespace Appalachia.Editor.Preferences.API
{
    internal struct bool_EPAPI : IEditorPreferenceAPI<bool>
    {
        public bool Get(string key, bool defaultValue, bool low, bool high)
        {
            return EditorPrefs.GetBool(key, defaultValue);
        }

        public void Save(string key, bool value, bool low, bool high)
        {
            EditorPrefs.SetBool(key, value);
        }

        public bool Draw(string label, bool value, bool low, bool high)
        {
            return EditorGUILayout.ToggleLeft(label, value);
        }
    }
}
