/*using Enemy;
using UnityEditor;
using UnityEngine;

namespace Editors
{
    [CustomEditor(typeof(EnemyAttacker))]
    public class EnemyAttackerEditor : Editor
    {
        private EnemyAttacker _attacker;
        private void OnSceneGUI()
        {
            _attacker = (EnemyAttacker)target;
            SetAttackRange();
            SetAttackFOV();
        }

        private void SetAttackRange()
        {
            Handles.color = Color.yellow;
            Handles.DrawWireArc(_attacker.transform.position, Vector3.up, Vector3.forward,  360f, _attacker.AttackRange);
        }
        
        private void SetAttackFOV()
        {
            Handles.color = Color.red;
            var pos = _attacker.transform.position;
            var angle = _attacker.transform.eulerAngles;
            
            Vector3 viewAngle01 = DirectionFromAngle(angle.y, -_attacker.AttackAngle / 2);
            Vector3 viewAngle02 = DirectionFromAngle(angle.y, _attacker.AttackAngle / 2);
            
            Handles.DrawWireArc(pos, Vector3.up, viewAngle01 , _attacker.AttackAngle, _attacker.AttackRange);
            Handles.DrawLine(pos, pos + viewAngle01 * _attacker.AttackRange);
            Handles.DrawLine(pos, pos + viewAngle02 * _attacker.AttackRange);
        }
        private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}*/
