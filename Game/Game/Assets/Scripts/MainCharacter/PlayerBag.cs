using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBag : MonoBehaviour
{

    [SerializeField] private GameObject firstBox;
    [SerializeField] private GameObject secondBox;
    [SerializeField] private GameObject thirddBox;

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

        this.currentWeaponIndex = 0;
    }

    private int FindNextAvailableWeapon(int position)
    {

        for (int i = 0; i < this.weapons.Count; ++i)
        {
            ++position;
            if (position >= this.weapons.Count)
                position = 0;
            if (this.weapons[position].GetNumber() > 0)
                return position;
        }

        return -1;
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

        this.UpdateCanvasElement();
    }

    private int CountAvailableWeapons()
    {
        int count = 0;

        foreach (Weapon weapon in this.weapons)
        {
            if (weapon.GetNumber() > 0)
                ++count;
        }

        return count;
    }

    private void UpdateCanvasElement()
    {
        GameObject[] boxes = {this.firstBox, this.secondBox, this.thirddBox};
        int position = this.currentWeaponIndex, weaponCount = this.CountAvailableWeapons();

        for (int i = 0; i < boxes.Length && weaponCount > 0; ++i)
        {
            if (position < 0)
                break;



            --weaponCount;
            position = this.FindNextAvailableWeapon(position);
        }
    }

    public bool UseCurrentWeaponToAttack()
    {
        if (this.currentWeaponIndex < 0)
            return false;
        return this.weapons[this.currentWeaponIndex].Attack();
    }

    public bool UseCurrentWeaponToAttackWithConsideringKeyboard()
    {
        if (this.currentWeaponIndex < 0)
            return false;
        return this.weapons[this.currentWeaponIndex].AttackWithConsideringKeyboard();
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

}
