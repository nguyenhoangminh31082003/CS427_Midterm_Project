using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Trigger 12");
        if (other.gameObject.GetComponent<EnemyHealth>()) {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(this.gameObject, 1);
        }
    }
}
