using Common;
using UnityEditor;
using UnityEngine;

namespace Editors
{
    [CustomEditor(typeof(Health))]
    public class HealthEditor : Editor
    {
        private Health _health;
        private void OnSceneGUI()
        {
            _health = (Health)target;
            SetWeakPointFOV();
        }

        private void SetWeakPointFOV()
        {
            Handles.color = Color.blue;
            var pos = _health.transform.position;
            var angle = _health.transform.eulerAngles;
            
            Vector3 viewAngle01 = DirectionFromAngle(angle.y, -_health.weakPointAngle / 2 + _health.weakPointPosition);
            Vector3 viewAngle02 = DirectionFromAngle(angle.y, _health.weakPointAngle / 2 + _health.weakPointPosition);
            
            Handles.DrawWireArc(pos, Vector3.up, viewAngle01 , _health.weakPointAngle, 5);
            Handles.DrawLine(pos, pos + viewAngle01 * 5);
            Handles.DrawLine(pos, pos + viewAngle02 * 5);
        }
        
        private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;
        
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}
