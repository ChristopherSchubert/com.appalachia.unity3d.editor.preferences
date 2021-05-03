#region

using UnityEditor;
using UnityEngine;

#endregion

namespace Appalachia.Editor.Preferences.API
{
    internal struct Gradient_EPAPI : IEditorPreferenceAPI<Gradient>
    {
        public Gradient Get(string key, Gradient defaultValue, Gradient low, Gradient high)
        {
            return GetGradient(key, defaultValue);
        }

        public void Save(string key, Gradient value, Gradient low, Gradient high)
        {
            var modeKey = $"{key}.mode";
            EditorPrefs.SetBool(modeKey, value.mode == GradientMode.Blend);

            var colorBaseKey = $"{key}.color";
            var alphaBaseKey = $"{key}.alpha";

            EditorPrefs.SetInt(colorBaseKey, value.colorKeys.Length);
            EditorPrefs.SetInt(alphaBaseKey, value.alphaKeys.Length);

            for (var i = 0; i < value.colorKeys.Length; i++)
            {
                var colorKey = $"{colorBaseKey}.{i}.value";
                var timeKey = $"{colorBaseKey}.{i}.time";

                EditorPrefs.SetInt(colorKey, (int) ToHex(value.colorKeys[i].color));
                EditorPrefs.SetFloat(timeKey, value.colorKeys[i].time);
            }

            for (var i = 0; i < value.alphaKeys.Length; i++)
            {
                var alphaKey = $"{alphaBaseKey}.{i}.value";
                var timeKey = $"{alphaBaseKey}.{i}.time";

                EditorPrefs.SetFloat(alphaKey, value.alphaKeys[i].alpha);
                EditorPrefs.SetFloat(timeKey,  value.alphaKeys[i].time);
            }
        }

        public Gradient Draw(string label, Gradient value, Gradient low, Gradient high)
        {
            var gradient = EditorGUILayout.GradientField(label, value);

            return gradient;
        }

        private static Gradient GetGradient(string key, Gradient defaultValue)
        {
            var gradient = new Gradient();

            var modeKey = $"{key}.mode";
            gradient.mode = EditorPrefs.GetBool(modeKey, true) ? GradientMode.Blend : GradientMode.Fixed;

            var colorBaseKey = $"{key}.color";
            var alphaBaseKey = $"{key}.alpha";

            var colorCount = EditorPrefs.GetInt(colorBaseKey, 2);
            var alphaCount = EditorPrefs.GetInt(alphaBaseKey, 2);

            var colorKeys = new GradientColorKey[colorCount];
            var alphaKeys = new GradientAlphaKey[alphaCount];

            for (var i = 0; i < colorCount; i++)
            {
                var colorKey = $"{colorBaseKey}.{i}.value";
                var timeKey = $"{colorBaseKey}.{i}.time";

                var color = EditorPrefs.GetInt(colorKey, (int) ToHex(i == 0 ? Color.black : Color.white));
                var time = EditorPrefs.GetFloat(timeKey, i);

                colorKeys[i] = new GradientColorKey(ToRGBA((uint) color), time);
            }

            for (var i = 0; i < alphaCount; i++)
            {
                var alphaKey = $"{alphaBaseKey}.{i}.value";
                var timeKey = $"{alphaBaseKey}.{i}.time";

                var alpha = EditorPrefs.GetFloat(alphaKey, 1.0f);
                var time = EditorPrefs.GetFloat(timeKey,   i);

                alphaKeys[i] = new GradientAlphaKey(alpha, time);
            }

            gradient.SetKeys(colorKeys, alphaKeys);

            return gradient;
        }

        public static uint ToHex(Color c)
        {
            return ((uint) (c.a * 255) << 24) |
                   ((uint) (c.r * 255) << 16) |
                   ((uint) (c.g * 255) << 8) |
                   (uint) (c.b * 255);
        }

        public static Color ToRGBA(uint hex)
        {
            return new Color(
                ((hex >> 16) & 0xff) / 255f, // r
                ((hex >> 8) & 0xff) / 255f,  // g
                (hex & 0xff) / 255f,         // b
                ((hex >> 24) & 0xff) / 255f  // a
            );
        }
    }
}
