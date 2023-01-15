using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBulletSc : MonoBehaviour
{
    public int attackDamage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealthSystem>().EnemyTakeDamage(attackDamage);
            Destroy(gameObject);
        }
    }
}
