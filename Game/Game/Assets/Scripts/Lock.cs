using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Interactable
{
    [SerializeField] KeyManager.KeyItem _requiredKey;

    protected override void Interact()
    {
        if (KeyManager.Instance.CheckRequiredItem(_requiredKey)) {
            Destroy(gameObject);
        }
    }
}
