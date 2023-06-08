/*using Common;
using UnityEditor;
using UnityEngine;

namespace Editors
{
    [CustomEditor(typeof(Health))]
    public class HealthEditor : Editor
    {
        private Health _health;

        #region SerializedProperties

        private SerializedProperty _maxHealth;
        private SerializedProperty _profit;
        private SerializedProperty _weakPointEnabled;
        private SerializedProperty _weakPointDamageAmplification;
        private SerializedProperty _weakPointAngle;
        private SerializedProperty _weakPointPosition;
        
        private SerializedProperty _getHitFb;
        private SerializedProperty _dieFb;
        
        private SerializedProperty _healthBarCanvas;

        private bool _feedbackList;
        #endregion

        private void OnEnable()
        {
            _health = (Health)target;
            
            _maxHealth = serializedObject.FindProperty("maxHealth");
            _profit = serializedObject.FindProperty("profit");
            _weakPointEnabled = serializedObject.FindProperty("WeakPointEnabled");
            _weakPointDamageAmplification = serializedObject.FindProperty("weakPointDamageAmplification");
            _weakPointAngle = serializedObject.FindProperty("WeakPointAngle");
            _weakPointPosition = serializedObject.FindProperty("WeakPointPosition");

            _getHitFb = serializedObject.FindProperty("getHitFb");
            _dieFb = serializedObject.FindProperty("dieFb");
            
            _healthBarCanvas = serializedObject.FindProperty("healthBarCanvas");
            
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.PropertyField(_maxHealth);
            EditorGUILayout.PropertyField(_profit);
            EditorGUILayout.PropertyField(_healthBarCanvas);
            if (_health.WeakPointEnabled) EditorGUILayout.Space(10);
            EditorGUILayout.PropertyField(_weakPointEnabled);
            if (_health.WeakPointEnabled)
            {
                
                EditorGUILayout.PropertyField(_weakPointDamageAmplification);
                EditorGUILayout.PropertyField(_weakPointAngle);
                EditorGUILayout.PropertyField(_weakPointPosition);
                EditorGUILayout.Space(10);
            }
            _feedbackList = EditorGUILayout.BeginFoldoutHeaderGroup(_feedbackList, "Feedbacks");
            if (_feedbackList)
            {
                EditorGUILayout.PropertyField(_getHitFb);
                EditorGUILayout.PropertyField(_dieFb);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void OnSceneGUI()
        {
            SetWeakPointFOV();
        }

        private void SetWeakPointFOV()
        {
            if (!_health.WeakPointEnabled) return;
            
            Handles.color = Color.blue;
            var pos = _health.transform.position;
            var angle = _health.transform.eulerAngles;
            
            Vector3 viewAngle01 = DirectionFromAngle(angle.y, -_health.WeakPointAngle / 2 + _health.WeakPointPosition);
            Vector3 viewAngle02 = DirectionFromAngle(angle.y, _health.WeakPointAngle / 2 + _health.WeakPointPosition);
            
            Handles.DrawWireArc(pos, Vector3.up, viewAngle01 , _health.WeakPointAngle, 5);
            Handles.DrawLine(pos, pos + viewAngle01 * 5);
            Handles.DrawLine(pos, pos + viewAngle02 * 5);
        }
        
        private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;
        
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}*/
