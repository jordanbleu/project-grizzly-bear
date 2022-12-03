using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Attributes
{
    public class ReadOnlyAttribute : Attribute
    { 
        // do nothing.  Uncomment the stuff below to bring this back.
        // this breaks unity builds cuz i did it wrong and i'm too lazy to figure out 
        // how to fix it.
    }
    //public class ReadOnlyAttribute : PropertyAttribute
    //{
    //    // apparently nothing?  Idk I didn't write this.
    //}

    //[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    //public class ReadOnlyDrawer : PropertyDrawer
    //{
    //    public override float GetPropertyHeight(SerializedProperty property,
    //                                            GUIContent label)
    //    {
    //        return EditorGUI.GetPropertyHeight(property, label, true);
    //    }

    //    //public override void OnGUI(Rect position,
    //    //                           SerializedProperty property,
    //    //                           GUIContent label)
    //    //{
    //    //    GUI.enabled = false;
    //    //    EditorGUI.PropertyField(position, property, label, true);
    //    //    GUI.enabled = true;
    //    //}
    //}
}
