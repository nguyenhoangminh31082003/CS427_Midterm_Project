using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // temporary
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

    public void TakeDamage(GameObject damageSource, int damage) {
        currentHealth -= damage;
        knockback.GetKnockedBack(damageSource.transform, 15f);
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
