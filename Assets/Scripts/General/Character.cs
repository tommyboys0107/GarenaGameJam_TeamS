using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("°ò¥»ÄÝ©Ê")]
    public float maxHealth;
    public float currentHealth;


    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float Damage)
    {
        currentHealth -= Damage;
    }
}
