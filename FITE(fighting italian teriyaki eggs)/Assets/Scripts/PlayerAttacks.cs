using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private float attackSize = 0.5f;
    [SerializeField]
    private LayerMask enemyLayers;
    [HideInInspector]
    public Vector2 direction;

    private IDictionary<string, Attack> grapplerAttacks = new Dictionary<string, Attack>()
    {
        {"Light Up", new Attack(7, 100, 0.7f)},
        {"Light Forward", new Attack(7, 100, 0.8f)},
        {"Light Down", new Attack(8, 2000, 0.8f)}
    };

    public async void LightAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            await GeneralAttack("Light");

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

    private class Attack
    {
        public float damage;
        public int speedInMiliseconds;
        public float range;
        public Attack(float damage, int speedInMiliseconds, float range)
        {
            this.damage = damage;
            this.speedInMiliseconds = speedInMiliseconds;
            this.range = range;
        }
    }

    private async Task GeneralAttack(string attackSpeed)
    {
        string dir;
        switch (direction.y)
        {
            case > 0.3f:
                attackPoint.localPosition = Vector3.up * 0.7f;
                dir = "Up";
                break;
            default:
                attackPoint.localPosition = Vector3.zero;
                dir = "Forward";
                break;
            case < -0.3f:
                attackPoint.localPosition = Vector3.down * 0.35f;
                dir = "Down";
                break;
        }
        Attack currentAttack = grapplerAttacks[attackSpeed + " " + dir];
        switch (direction.x)
        {
            case > 0:
                attackPoint.localPosition += Vector3.right * currentAttack.range;
                break;
            case < 0:
                attackPoint.localPosition += Vector3.left * currentAttack.range;
                break;
        }
        await Task.Delay(currentAttack.speedInMiliseconds);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackSize, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.transform != transform)
            {
                enemy.GetComponent<Health>().Damage(currentAttack.damage);
            }

        }
    }
}
