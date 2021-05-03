#region

using System;
using System.Collections.Generic;
using Appalachia.Editor.Preferences.API;
using UnityEngine;

#endregion

namespace Appalachia.Editor.Preferences.States
{
    internal class EditorPreferenceState<T> : EditorPreferenceStateBase
    {
        private readonly Dictionary<string, Pref<T>> _values = new Dictionary<string, Pref<T>>();

        private readonly List<Pref<T>> _sortedValues = new List<Pref<T>>();

        private bool _sorted;

        public EditorPreferenceState()
        {
            var typeT = typeof(T);

            if (typeT == null)
            {
                throw new TypeAccessException();
            }

            if (typeT == typeof(bool))
            {
                API = new bool_EPAPI() as IEditorPreferenceAPI<T>;
            }
            else if (typeT == typeof(Bounds))
            {
                API = new Bounds_EPAPI() as IEditorPreferenceAPI<T>;
            }
            else if (typeT == typeof(Color))
            {
                API = new Color_EPAPI() as IEditorPreferenceAPI<T>;
            }
            else if (typeT == typeof(Gradient))
            {
                API = new Gradient_EPAPI() as IEditorPreferenceAPI<T>;
            }
            else if (typeT == typeof(double))
            {
                API = new double_EPAPI() as IEditorPreferenceAPI<T>;
            }
            else if (typeT == typeof(float))
            {
                API = new float_EPAPI() as IEditorPreferenceAPI<T>;
            }
            else if (typeT == typeof(Vector2))
            {
                API = new Vector2_EPAPI() as IEditorPreferenceAPI<T>;
            }
            else if (typeT == typeof(Vector3))
            {
                API = new Vector3_EPAPI() as IEditorPreferenceAPI<T>;
            }
            else if (typeT == typeof(Vector4))
            {
                API = new Vector4_EPAPI() as IEditorPreferenceAPI<T>;
            }
            else if (typeT == typeof(int))
            {
                API = new int_EPAPI() as IEditorPreferenceAPI<T>;
            }
            else if (typeT == typeof(Quaternion))
            {
                API = new Quaternion_EPAPI() as IEditorPreferenceAPI<T>;
            }
            else if (typeT == typeof(string))
            {
                API = new string_EPAPI() as IEditorPreferenceAPI<T>;
            }
            else if (typeT.IsEnum || (typeT == typeof(Enum)))
            {
                API = new Enum_EPAPI<T>();
            }
            else
            {
                throw new NotSupportedException(typeT.Name);
            }
        }

        public IReadOnlyDictionary<string, Pref<T>> Values => _values;

        public IReadOnlyList<Pref<T>> SortedValues => _sortedValues;

        public PrefComparer Comparer { get; } = new PrefComparer();

        public IEditorPreferenceAPI<T> API { get; }

        public void Sort()
        {
            if (_values.Count == 0)
            {
                return;
            }

            if (_sorted)
            {
                return;
            }

            _sorted = true;
            _sortedValues.Clear();

            foreach (var value in _values)
            {
                _sortedValues.Add(value.Value);
            }

            _sortedValues.Sort(Comparer);
        }

        public void Add(string key, Pref<T> value)
        {
            if (_values.ContainsKey(key))
            {
                _values[key] = value;
            }
            else
            {
                _values.Add(key, value);
            }

            _sorted = false;
        }

        public override void Awake()
        {
            foreach (var value in _values)
            {
                value.Value.WakeUp();
            }
        }

        public class PrefComparer : Comparer<Pref<T>>
        {
            public override int Compare(Pref<T> x, Pref<T> y)
            {
                if ((x == null) && (y == null))
                {
                    return 0;
                }

                if ((x != null) && (y == null))
                {
                    return -1;
                }

                if ((x == null) && (y != null))
                {
                    return 1;
                }

                var order = x.Order.CompareTo(y.Order);

                return order != 0 ? order : string.Compare(x.Key, y.Key, StringComparison.Ordinal);
            }
        }
    }
}
