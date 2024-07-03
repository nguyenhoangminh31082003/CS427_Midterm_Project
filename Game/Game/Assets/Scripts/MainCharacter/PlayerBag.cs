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
    [SerializeField] private int gateKeyCount;
    [SerializeField] private int chestKeyCount;

    private bool weaponBoxesUIChangeRequired;
    private List<Weapon> weapons;
    private double totalWeight;

    // Start is called before the first frame update
    void Start()
    {
        this.gateKeyCount = 0;

        this.chestKeyCount = 0;

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

        this.weaponBoxesUIChangeRequired = false;
    }

    public bool changeGateKeyCount(int delta)
    {
        int newGateKeyCount = this.gateKeyCount + delta;
        if (newGateKeyCount < 0)
            return false;
        this.gateKeyCount = newGateKeyCount;
        return true;
    }

    public bool changeChestKeyCount(int delta)
    {
        int newChestKeyCount = this.chestKeyCount + delta;
        if (newChestKeyCount < 0)
            return false;
        this.gateKeyCount = newChestKeyCount;
        return true;
    }

    private int FindNextAvailableWeapon(int position)
    {

        //foreach (Weapon playerWeapon in weapons)
        //{
        //    Debug.Log(playerWeapon.GetWeaponName() + " " + playerWeapon.GetNumber());
        //}

        //Debug.Log("Number of weapons " + this.weapons.Count);

        for (int i = 0; i < this.weapons.Count; ++i)
        {
            ++position;
            if (position >= this.weapons.Count)
                position = 0;
            //Debug.Log("position " + position);
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
                foreach (Weapon playerWeapon in weapons)
                {
                    playerWeapon.IncreaseNumber(1);
                }
                weapon.StartUsing();

                this.weaponBoxesUIChangeRequired = true;
            }
        }

        this.UpdateCanvasElements();
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

    private void UpdateCanvasElements()
    {
        if (this.weaponBoxesUIChangeRequired)
        {
            GameObject[] boxes = { this.firstBox, this.secondBox, this.thirddBox };
            int position = this.currentWeaponIndex, weaponCount = this.CountAvailableWeapons();

            for (int i = 0; i < boxes.Length; ++i)
            {

                foreach (Transform child in boxes[i].transform)
                {
                    Destroy(child.gameObject);
                }

                if (position < 0 || weaponCount <= 0)
                    continue;

                //Debug.Log(position + " " + i + " " + this.weapons[i].GetWeaponName() + " " + boxes[i].name);

                //this.weapons[i].DisplayInCanvas(boxes[position]);
                this.weapons[position].DisplayInCanvas(boxes[i]);

                --weaponCount;

                //Debug.Log("before position: " + position);

                position = this.FindNextAvailableWeapon(position);

                //Debug.Log("after position: " + position);
            }

            this.weaponBoxesUIChangeRequired = false;
        }
    }

    public bool MoveToTheNextWeaponAsTheCurrentWeapon()
    {
        Debug.Log(this.currentWeaponIndex);
        if (this.currentWeaponIndex < 0)
            return false;
        //Debug.Log(this.currentWeaponIndex);
        this.weapons[this.currentWeaponIndex].StopUsing();
        this.weaponBoxesUIChangeRequired = true;
        this.currentWeaponIndex = this.FindNextAvailableWeapon(this.currentWeaponIndex);
        this.weapons[this.currentWeaponIndex].StartUsing();
        return true;
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
