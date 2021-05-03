#region

using UnityEditor;

#endregion

namespace Appalachia.Editor.Preferences.API
{
    internal struct string_EPAPI : IEditorPreferenceAPI<string>
    {
        public string Get(string key, string defaultValue, string low, string high)
        {
            return EditorPrefs.GetString(key, defaultValue);
        }

        public void Save(string key, string value, string low, string high)
        {
            EditorPrefs.SetString(key, value);
        }

        public string Draw(string label, string value, string low, string high)
        {
            return EditorGUILayout.TextField(label, value);
        }
    }
}
