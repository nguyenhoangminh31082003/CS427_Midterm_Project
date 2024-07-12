using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;

    public void Destruct() {
        AudioManager.Instance.PlaySFX("box_destroy");
        GetComponent<PickUpSpawner>().DropItems();
        Instantiate(destroyVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
