using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] int baseDamage = 10;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float attackRadius = 0.5f;
    [SerializeField] Transform attackPoint;
    public bool debugMode;

    private void PerformSweepAttack()
    {
        int steps = Mathf.CeilToInt(attackRange / attackRadius);

        for (int i = 0; i <= steps; i++)
        {
            Vector3 sweepPosition = attackPoint.position + attackPoint.forward * (i * attackRadius);

            RaycastHit[] hits = Physics.SphereCastAll(sweepPosition, attackRadius, attackPoint.forward, 0.1f, enemyLayer);

            foreach (RaycastHit hit in hits)
            {
                BaseEnemy enemy = hit.collider.GetComponent<BaseEnemy>();
                Debug.Log("enemy: " + enemy);
                if (enemy != null)
                {
                    int weaponDamage = CalculateDamage();
                    enemy.TakeDamage(weaponDamage);
                    if (debugMode) Debug.Log("Dealt " + weaponDamage + " damage to " + hit.collider.name);
                }
            }
        }
    }

    private int CalculateDamage()
    {
        AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(1);
        if (stateInfo.IsName("Skill1Stage1")) return baseDamage;
        if (stateInfo.IsName("Skill1Stage2")) return Mathf.RoundToInt(baseDamage * 1.5f);
        if (stateInfo.IsName("Skill1Stage3")) return Mathf.RoundToInt(baseDamage * 2.5f);
        return baseDamage;
    }

    public void OnAttack()
    {
        if (debugMode) Debug.Log("performing sweep");
        PerformSweepAttack();
    }

    private void OnDrawGizmos()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
            int steps = Mathf.CeilToInt(attackRange / attackRadius);
            for (int i = 0; i <= steps; i++)
            {
                Vector3 sweepPosition = attackPoint.position + attackPoint.forward * (i * attackRadius);
                Gizmos.DrawWireSphere(sweepPosition, attackRadius);
                if (i < steps)
                {
                    Vector3 nextSweepPosition = attackPoint.position + attackPoint.forward * ((i + 1) * attackRadius);
                    Gizmos.DrawLine(sweepPosition, nextSweepPosition);
                }
            }
        }
    }
}
