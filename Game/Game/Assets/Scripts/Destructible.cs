using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;

    // private void OnTriggerEnter2D(Collider2D other) {
    //     if (other.gameObject.GetComponent<DamageSource>()) {
    //         Instantiate(destroyVFX, transform.position, Quaternion.identity);
    //         Destroy(gameObject);
    //     }
    // }
    
    private void OnCollisionEnter2D(Collision2D other) {
        GetComponent<PickUpSpawner>().DropItems();
        Instantiate(destroyVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }
}
