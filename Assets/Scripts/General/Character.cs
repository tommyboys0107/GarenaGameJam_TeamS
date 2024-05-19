using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class Character : MonoBehaviour
{
    [Header("°ò¥»ÄÝ©Ê")]
    public float maxHealth = 100;
    public float currentHealth;
    public UnityEvent<Character> OnHealthChange;

    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnHealthChange?.Invoke(this);
        Debug.Log("Player Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void AddHealth(int heart)
    {
        currentHealth += heart;
        OnHealthChange?.Invoke(this);
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    void Die()
    {
        Debug.Log("Player Died");

    }
}
