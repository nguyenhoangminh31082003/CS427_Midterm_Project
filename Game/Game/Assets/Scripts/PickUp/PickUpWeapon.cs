using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : Interactable
{
    [SerializeField] private ItemWeaponType weaponType;
    public enum ItemWeaponType {
        Sword,
        Bow,
    }
    protected override void Interact()
    {
        Debug.Log("Picked "+weaponType.ToString());
        gameManager.CollisionHandler(MainCharacter.Instance.tag, MainCharacter.Instance.name, "Item", this.transform.name);
    }

    public ItemWeaponType GetPickUpType()
    {
        return weaponType;
    }

}
