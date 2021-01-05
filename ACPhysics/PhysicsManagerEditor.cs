using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ACPhysics
{

    [CustomEditor(typeof(PhysicsManager))]
    public class PhysicsManagerEditor : Editor
    {
        SerializedProperty groundMask, checkGround, groundedDebugCircle, radius, offset, color, gravity, jumpInverseDamping, fallingDamping, jumpIntensity, fallIntensity;
        SerializedProperty moveIntensity;


        bool showCircle;

        private void OnEnable()
        {
            groundMask = serializedObject.FindProperty("groundMask");
            checkGround = serializedObject.FindProperty("checkGround");
            groundedDebugCircle = serializedObject.FindProperty("groundedDebugCircle");
            radius = serializedObject.FindProperty("radius");
            offset = serializedObject.FindProperty("offset");
            color = serializedObject.FindProperty("color");
            gravity = serializedObject.FindProperty("gravity");
            jumpInverseDamping = serializedObject.FindProperty("jumpInverseDamping");
            fallingDamping = serializedObject.FindProperty("fallingDamping");
            jumpIntensity = serializedObject.FindProperty("jumpIntensity");
            fallIntensity = serializedObject.FindProperty("fallIntensity");
            moveIntensity = serializedObject.FindProperty("moveIntensity");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.LabelField("Check ground");
            EditorGUILayout.PropertyField(groundMask);
            EditorGUILayout.PropertyField(checkGround);
            EditorGUILayout.PropertyField(groundedDebugCircle);
            EditorGUILayout.LabelField("Check ground circle");
            EditorGUILayout.PropertyField(radius);
            EditorGUILayout.PropertyField(offset);
            EditorGUILayout.PropertyField(color);
            if (showCircle = EditorGUILayout.Foldout(showCircle, "Check ground circle"))
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(gravity);
                EditorGUILayout.PropertyField(jumpInverseDamping);
                EditorGUILayout.PropertyField(fallingDamping);
                EditorGUILayout.PropertyField(jumpIntensity);
                EditorGUILayout.PropertyField(fallIntensity);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.PropertyField(moveIntensity);
            serializedObject.ApplyModifiedProperties();
        }
    }

}