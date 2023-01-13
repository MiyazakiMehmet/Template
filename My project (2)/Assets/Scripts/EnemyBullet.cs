using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int e_AttackDamage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealthSystem>().PlayerTakeDamage(e_AttackDamage);
            Destroy(gameObject);
        }
    }
}
