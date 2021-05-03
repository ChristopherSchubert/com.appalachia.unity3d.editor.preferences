#region

using System;
using Appalachia.Editor.Preferences.States;
using UnityEngine;

#endregion

namespace Appalachia.Editor.Preferences
{
#if APPA_ODIN_INSPECTOR
    [ShowInInspector, InlineProperty, HideReferenceObjectPicker]
#endif
    [Serializable]
    public sealed class Pref<T>
    {
#if APPA_ODIN_INSPECTOR
        [HideLabel, InlineProperty, OnValueChanged(nameof(UIApplyValue))]
        [InlineButton(nameof(Reset), " Reset ")]
#endif
        [SerializeField] private T _value;

        private bool _isAwake;

        private readonly EditorPreferenceState<T> _editorPreferences;

        private readonly T _defaultValue;
        private readonly T _low;
        private readonly T _high;
        private readonly string _key;
        private readonly string _grouping;
        private readonly string _label;
        private string _niceLabel;
        private readonly int _order;
        private bool _reset;

        internal Pref(
            string key,
            string grouping,
            string label,
            T defaultValue,
            T low,
            T high,
            int order,
            bool reset)
        {
            _key = key;
            _grouping = grouping;
            _label = label;
            _defaultValue = defaultValue;
            _low = low;
            _high = high;
            _order = order;
            _editorPreferences = EditorPreferenceStates.Get<T>();
            _reset = reset;
        }

        internal bool IsAwake => _isAwake;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                if (_isAwake)
                {
                    _editorPreferences.API.Save(_key, _value, _low, _high);
                }
            }
        }

        public T v
        {
            get => Value;
            set => Value = value;
        }

        internal string Key => _key;
        internal string Grouping => _grouping;
        internal string Label => _label;

        internal string NiceLabel
        {
            get => _niceLabel;
            set => _niceLabel = value;
        }

        internal int Order => _order;

        internal T Low => _low;
        internal T High => _high;

        private void UIApplyValue()
        {
            if (_isAwake)
            {
                _editorPreferences.API.Save(_key, _value, _low, _high);
            }
        }

        public void WakeUp()
        {
            _value = _editorPreferences.API.Get(_key, _defaultValue, _low, _high);

            ExecuteResetIfNecessary();
            _isAwake = true;
        }

        private void Refresh()
        {
            if (_isAwake)
            {
                _value = _editorPreferences.API.Get(_key, _defaultValue, _low, _high);
            }
        }

        private void Reset()
        {
            _reset = true;
            ExecuteResetIfNecessary();
        }

        private void ExecuteResetIfNecessary()
        {
            if (_reset)
            {
                _value = _defaultValue;

                if (_isAwake)
                {
                    _editorPreferences.API.Save(_key, _defaultValue, _low, _high);
                    _reset = false;
                }
            }
        }
    }
}
