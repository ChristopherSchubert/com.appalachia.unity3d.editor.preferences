#region

using System;
using Appalachia.Editor.Preferences.States;
using UnityEditor;

#endregion

namespace Appalachia.Editor.Preferences
{
    [InitializeOnLoad]
    public static class EditorPreferences
    {
        static EditorPreferences()
        {
            EditorApplication.delayCall += EditorPreferenceStates.Awake;
        }

        public static Pref<TR> Register<TR>(
            string grouping,
            string label,
            TR dv,
            TR low = default,
            TR high = default,
            int order = 0,
            bool reset = false)
        {
            var splits = label.Split('_');
            label = splits[splits.Length - 1];
            var key =
                $"{grouping.ToLower().Replace(" ", string.Empty).Trim()}.{label.ToLower().Replace(" ", string.Empty).Trim()}";

            EditorPreferenceStates._keys.Add(key);
            EditorPreferenceStates._groupings.Add(grouping);

            if (InternalRegistration(
                key,
                grouping,
                label,
                dv,
                low,
                high,
                order,
                reset,
                EditorPreferenceStates._bools,
                out var br
            ))
            {
                return br as Pref<TR>;
            }

            if (InternalRegistration(
                key,
                grouping,
                label,
                dv,
                low,
                high,
                order,
                reset,
                EditorPreferenceStates._ints,
                out var ir
            ))
            {
                return ir as Pref<TR>;
            }

            if (InternalRegistration(
                key,
                grouping,
                label,
                dv,
                low,
                high,
                order,
                reset,
                EditorPreferenceStates._strings,
                out var sr
            ))
            {
                return sr as Pref<TR>;
            }

            if (InternalRegistration(
                key,
                grouping,
                label,
                dv,
                low,
                high,
                order,
                reset,
                EditorPreferenceStates._bounds,
                out var bor
            ))
            {
                return bor as Pref<TR>;
            }

            if (InternalRegistration(
                key,
                grouping,
                label,
                dv,
                low,
                high,
                order,
                reset,
                EditorPreferenceStates._colors,
                out var cr
            ))
            {
                return cr as Pref<TR>;
            }

            if (InternalRegistration(
                key,
                grouping,
                label,
                dv,
                low,
                high,
                order,
                reset,
                EditorPreferenceStates._gradients,
                out var gr
            ))
            {
                return gr as Pref<TR>;
            }

            if (InternalRegistration(
                key,
                grouping,
                label,
                dv,
                low,
                high,
                order,
                reset,
                EditorPreferenceStates._quaternions,
                out var qr
            ))
            {
                return qr as Pref<TR>;
            }

            if (InternalRegistration(
                key,
                grouping,
                label,
                dv,
                low,
                high,
                order,
                reset,
                EditorPreferenceStates._doubles,
                out var fd
            ))
            {
                return fd as Pref<TR>;
            }

            if (InternalRegistration(
                key,
                grouping,
                label,
                dv,
                low,
                high,
                order,
                reset,
                EditorPreferenceStates._floats,
                out var fr
            ))
            {
                return fr as Pref<TR>;
            }

            if (InternalRegistration(
                key,
                grouping,
                label,
                dv,
                low,
                high,
                order,
                reset,
                EditorPreferenceStates._float2s,
                out var fr2
            ))
            {
                return fr2 as Pref<TR>;
            }

            if (InternalRegistration(
                key,
                grouping,
                label,
                dv,
                low,
                high,
                order,
                reset,
                EditorPreferenceStates._float3s,
                out var fr3
            ))
            {
                return fr3 as Pref<TR>;
            }

            if (InternalRegistration(
                key,
                grouping,
                label,
                dv,
                low,
                high,
                order,
                reset,
                EditorPreferenceStates._float4s,
                out var fr4
            ))
            {
                return fr4 as Pref<TR>;
            }

            if (InternalRegistrationEnum<TR>(key, grouping, label, dv, order, reset, out var e))
            {
                return e;
            }

            throw new NotSupportedException();
        }

        private static bool InternalRegistration<TR>(
            string key,
            string grouping,
            string label,
            object defaultValue,
            object low,
            object high,
            int order,
            bool reset,
            EditorPreferenceState<TR> cached,
            out Pref<TR> result)
        {
            if (defaultValue is TR dv)
            {
                if (cached.Values.ContainsKey(key))
                {
                    result = cached.Values[key];
                    return true;
                }

                var trLow = (TR) Convert.ChangeType(low,   typeof(TR));
                var trHigh = (TR) Convert.ChangeType(high, typeof(TR));

                var instance = new Pref<TR>(key, grouping, label, dv, trLow, trHigh, order, reset);

                if (EditorPreferenceStates._safeToAwaken)
                {
                    instance.WakeUp();
                }

                cached.Add(key, instance);
                result = instance;
                return true;
            }

            result = null;
            return false;
        }

        private static bool InternalRegistrationEnum<TR>(
            string key,
            string grouping,
            string label,
            object defaultValue,
            int order,
            bool reset,
            out Pref<TR> result)
        {
            var state = EditorPreferenceStates.GetEnumState<TR>();

            if (defaultValue is TR dv)
            {
                if (state.Values.ContainsKey(key))
                {
                    result = state.Values[key];
                    return true;
                }

                var instance = new Pref<TR>(key, grouping, label, dv, default, default, order, reset);

                if (EditorPreferenceStates._safeToAwaken)
                {
                    instance.WakeUp();
                }

                state.Add(key, instance);
                result = instance;
                return true;
            }

            result = null;
            return false;
        }
    }
}
