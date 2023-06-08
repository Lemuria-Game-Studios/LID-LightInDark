/*using Enemy;
using UnityEditor;
using UnityEngine;

namespace Editors
{
    [CustomEditor(typeof(EnemySensor))]
    public class EnemySensorEditor : Editor
    {
        private EnemySensor _sensor;
        private void OnSceneGUI()
        {
            _sensor = (EnemySensor)target;
            SetSightFOV();
            SetSensoryFOV();
            SetPlayerDetection();
        }

        private void SetPlayerDetection()
        {
            if (_sensor.IsPlayerDetected)
            {
                Handles.color = Color.black;
                Handles.DrawLine(_sensor.transform.position, _sensor.Target.transform.position);
            }
        }

        private void SetSensoryFOV()
        {
            Handles.color = Color.magenta;
            Handles.DrawWireArc(_sensor.transform.position, Vector3.up, Vector3.forward,  360f, _sensor.SensoryRange);
        }

        private void SetSightFOV()
        {
            Handles.color = Color.magenta;
            var pos = _sensor.transform.position;
            var angle = _sensor.transform.eulerAngles;
            
            Vector3 viewAngle01 = DirectionFromAngle(angle.y, -_sensor.SightAngle / 2);
            Vector3 viewAngle02 = DirectionFromAngle(angle.y, _sensor.SightAngle / 2);
            
            Handles.DrawWireArc(pos, Vector3.up, viewAngle01 , _sensor.SightAngle, _sensor.SightRange);
            Handles.DrawLine(pos, pos + viewAngle01 * _sensor.SightRange);
            Handles.DrawLine(pos, pos + viewAngle02 * _sensor.SightRange);
        }

        private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;
        
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}*/
