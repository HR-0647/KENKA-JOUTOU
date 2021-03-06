// Colorful FX - Unity Asset
// Copyright (c) 2015 - Thomas Hourdel
// http://www.thomashourdel.com

namespace Colorful.Editors
{
    using UnityEditor;

    [CustomEditor(typeof(GaussianBlur))]
    public class GaussianBlurEditor : BaseEffectEditor
    {
        SerializedProperty p_Passes;
        SerializedProperty p_Downscaling;
        SerializedProperty p_Amount;

        void OnEnable()
        {
            p_Passes = serializedObject.FindProperty("Passes");
            p_Downscaling = serializedObject.FindProperty("Downscaling");
            p_Amount = serializedObject.FindProperty("Amount");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(p_Passes);
            EditorGUILayout.PropertyField(p_Downscaling);
            EditorGUILayout.PropertyField(p_Amount);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
