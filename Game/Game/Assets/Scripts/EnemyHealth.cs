using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // temporary
    //public GameObject player;

    [SerializeField] private GameObject deathVFX;
    [SerializeField] private int initialHealth = 3;
    private int currentHealth;
    private Knockback knockback;
    private Flash flash;

    void Awake() {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }
    void Start()
    {
        currentHealth = initialHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        // Debug.Log(currentHealth);
        StartCoroutine(flash.FlashRoutine());
        CheckDeath();
    }

    public void CheckDeath() {
        if (currentHealth <= 0) {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
