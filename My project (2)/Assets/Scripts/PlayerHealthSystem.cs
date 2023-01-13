using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public bool playerAlive;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        playerAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < 0)
        {
            playerAlive = false;
        }
    }

    public void PlayerTakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
        }
    }
}
