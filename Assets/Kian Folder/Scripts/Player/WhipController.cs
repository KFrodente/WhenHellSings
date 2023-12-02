using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipController : MonoBehaviour
{
    public int healAmount;

    public int whipDamage;
    public float whipStunTime;
    public int knockbackPower;

    [HideInInspector] public List<Collider2D> hitEnemies;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && !hitEnemies.Contains(other))
        {
            other.GetComponent<EnemyParent>().TakeDamage(whipDamage, whipStunTime);
            other.GetComponent<EnemyParent>().Knockback(knockbackPower);
            hitEnemies.Add(other);

            if (PlayerWhip.instance.whipHealCounter <= 0) 
            { 
                PlayerHealthController.instance.Heal(healAmount);

                PlayerWhip.instance.whipHealCounter = PlayerWhip.instance.oWhipHealCooldown;
            }

        }
    }
}
