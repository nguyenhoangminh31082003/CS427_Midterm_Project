using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int initialHealth = 3;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = initialHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
    }

    private void DetectDeath() {
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
}
