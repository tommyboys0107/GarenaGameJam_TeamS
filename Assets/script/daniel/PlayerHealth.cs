using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth); 
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public int GetCurrentHealth()
    { 
        return currentHealth;
    }

    void Die()
    {
        Debug.Log("Player Died");

    }
}
