using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBag : MonoBehaviour
{

    [SerializeField] private int currentWeaponIndex;
    private List<Weapon> weapons;
    private double totalWeight;

    // Start is called before the first frame update
    void Start()
    {
        this.totalWeight = 0;

        this.weapons = new List<Weapon>();

        this.currentWeaponIndex = -1;

        foreach (Transform child in this.transform)
        {
            Weapon weapon = child.GetComponent<Weapon>();
            if (weapon != null && weapon is Weapon)
            {
                this.weapons.Add(weapon);
            }
        }

        if (this.weapons.Count > 0)
        {
            this.currentWeaponIndex = 0;

            Weapon weapon = this.weapons[this.currentWeaponIndex];

            if (weapon.GetNumber() == 0)
            {
                weapon.IncreaseNumber(1);
                weapon.StartUsing();
            }
        }
    }

    public double GetTotalWeight()
    {
        return this.totalWeight;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.currentWeaponIndex >= 0)
        {
           
            Weapon weapon = this.weapons[this.currentWeaponIndex];

            if (weapon.GetNumber() == 0)
            {
                weapon.IncreaseNumber(1);
                weapon.StartUsing();
            }
        }
    }

    public bool UseCurrentWeaponToAttack()
    {
        if (this.currentWeaponIndex < 0)
            return false;
        return this.weapons[this.currentWeaponIndex].Attack();
    }

    public bool IsAttacking()
    {
        if (this.currentWeaponIndex < 0)
            return false;
        return this.weapons[this.currentWeaponIndex].IsBeingUsedToAttack();
    }

    public void ChangeColorRecursively(Color color)
    {
        if (this.currentWeaponIndex >= 0)
        {
            this.weapons[this.currentWeaponIndex].ChangeColorRecursively(color);
        }
    }

    public Weapon GetCurrentWeapon()
    {
        if (this.currentWeaponIndex < 0)
            return null;
        return this.weapons[this.currentWeaponIndex];
    }
}
