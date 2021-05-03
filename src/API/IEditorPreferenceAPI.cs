namespace Appalachia.Editor.Preferences.API
{
    internal interface IEditorPreferenceAPI<T>
    {
        T Get(string key, T defaultValue, T low, T high);

        void Save(string key, T value, T low, T high);

        T Draw(string label, T value, T low, T high);
    }
}
