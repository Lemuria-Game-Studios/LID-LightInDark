using UnityEditor;
using UnityEngine;

namespace Enemy
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
            Handles.DrawWireArc(_attacker.transform.position, Vector3.up, Vector3.forward,  360f, _attacker.attackRange);
        }
        
        private void SetAttackFOV()
        {
            Handles.color = Color.red;
            var pos = _attacker.transform.position;
            Handles.DrawWireArc(pos, Vector3.up, DirectionFromAngle(_attacker.transform.eulerAngles.y, -_attacker.attackAngle / 2) , _attacker.attackAngle, _attacker.attackRange);
            Vector3 viewAngle01 = DirectionFromAngle(_attacker.transform.eulerAngles.y, -_attacker.attackAngle / 2);
            Vector3 viewAngle02 = DirectionFromAngle(_attacker.transform.eulerAngles.y, _attacker.attackAngle / 2);
        
            Handles.DrawLine(pos, pos + viewAngle01 * _attacker.attackRange);
            Handles.DrawLine(pos, pos + viewAngle02 * _attacker.attackRange);
        }
        private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}
