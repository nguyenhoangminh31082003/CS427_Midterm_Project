using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : Interactable
{
    [SerializeField] private ItemWeaponType weaponType;
    private enum ItemWeaponType {
        Sword,
        Bow,
    }
    protected override void Interact()
    {
        Debug.Log("Picked "+weaponType.ToString());
        // CALL MANAGER, USE WEAPON TYPE TO CHECK <======================================================================
    }

}
