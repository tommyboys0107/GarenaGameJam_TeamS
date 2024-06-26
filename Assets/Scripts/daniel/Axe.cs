using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public int damage = 10;

    private void Start()
    {
        Destroy(gameObject, 2f);
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered by: " + collision.name);

        if (collision.CompareTag("Player"))
        {
            Character playerHealth = collision.GetComponent<Character>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Player Health after damage: " + playerHealth.GetCurrentHealth());
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("BG"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 2f);
        }
    }
}
