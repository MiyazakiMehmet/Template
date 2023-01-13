using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int p_AttackDamage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealthSystem>().EnemyTakeDamage(p_AttackDamage);
            Destroy(gameObject);
        }
    }
}
