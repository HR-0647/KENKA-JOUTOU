// Colorful FX - Unity Asset
// Copyright (c) 2015 - Thomas Hourdel
// http://www.thomashourdel.com

namespace Colorful.Editors
{
    using UnityEditor;

    [CustomEditor(typeof(GradientRamp))]
    public class GradientRampEditor : BaseEffectEditor
    {
        SerializedProperty p_RampTexture;
        SerializedProperty p_Amount;

        void OnEnable()
        {
            p_RampTexture = serializedObject.FindProperty("RampTexture");
            p_Amount = serializedObject.FindProperty("Amount");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(p_RampTexture);
            EditorGUILayout.PropertyField(p_Amount);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
