using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Appalachia.Editor.Preferences.Editor
{
    internal abstract class PREFDrawer<T> : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            // Create property container element.
            var container = new VisualElement();

            // Create property fields.
            var valueField = new PropertyField(property.FindPropertyRelative("_value"), string.Empty);

            // Add fields to the container.
            container.Add(valueField);

            return container;
        }
    }
}
