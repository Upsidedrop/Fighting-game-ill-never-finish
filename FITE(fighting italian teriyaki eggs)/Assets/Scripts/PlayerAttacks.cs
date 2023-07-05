using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField]
    Transform attackPoint;
    [SerializeField]
    float attackSize = 0.5f;
    [SerializeField]
    LayerMask enemyLayers;
    [HideInInspector]
    public Vector2 direction;
    [SerializeField]
    float attackRange;
    [SerializeField]
    float damage;

    public void LightAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (direction.x)
            {
                case > 0:
                    attackPoint.localPosition = Vector2.right * attackRange;
                    break;
                case < 0:
                    attackPoint.localPosition = Vector2.left * attackRange;
                    break;
            }
            switch (direction.y)
            {
                case > 0.3f:
                    attackPoint.localPosition += Vector3.up * 0.7f;
                    break;
                default:
                    break;
                case < -0.3f:
                    attackPoint.localPosition += Vector3.down * 0.35f;
                    break;
            }
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackSize, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.transform != transform)
                {
                    enemy.GetComponent<Health>().Damage(damage);
                }

            }
        }

    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackSize);
    }
}
