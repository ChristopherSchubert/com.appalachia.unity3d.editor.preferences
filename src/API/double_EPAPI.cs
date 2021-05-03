#region

using System;
using UnityEditor;

#endregion

namespace Appalachia.Editor.Preferences.API
{
    internal struct double_EPAPI : IEditorPreferenceAPI<double>
    {
        public double Get(string key, double defaultValue, double low, double high)
        {
            return Round(EditorPrefs.GetFloat(key, Round(defaultValue)));
        }

        public void Save(string key, double value, double low, double high)
        {
            EditorPrefs.SetFloat(key, Round(value));
        }

        public double Draw(string label, double value, double low, double high)
        {
            var val = low != high
                ? EditorGUILayout.Slider(label, Round(value), (float) low, (float) high)
                : EditorGUILayout.DoubleField(label, Round(value));

            return Round(val);
        }

        private static double Round(float d)
        {
            return Math.Round(d, 4, MidpointRounding.AwayFromZero);
        }

        private static float Round(double d)
        {
            return (float) Math.Round(d, 4, MidpointRounding.AwayFromZero);
        }
    }
}
