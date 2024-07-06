using TMPro;
using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using JetBrains.Annotations;
public class PlayerBag : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI silverKeyCountText;
    [SerializeField] private TextMeshProUGUI goldenKeyCountText;

    [SerializeField] private GameObject sampleBox;
    [SerializeField] private GameObject canvasUIWeaponsContainer;
    [SerializeField] private int numberOfCanvasUIWeaponBoxes;

    [SerializeField] private int currentWeaponIndex;
    [SerializeField] private int chestKeyCount;
    [SerializeField] private int gateKeyCount;

    private List<GameObject> canvasUIWeaponBoxes;
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

        this.canvasUIWeaponBoxes = new List<GameObject>();

        for (int i = 0; i < numberOfCanvasUIWeaponBoxes; ++i)
        {
            GameObject duplicate = Instantiate(this.sampleBox, this.canvasUIWeaponsContainer.transform);

            RectTransform rectTransform = duplicate.GetComponent<RectTransform>();

            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - i * rectTransform.rect.height);
            }

            duplicate.name = "Weapon box container " + i.ToString();

            duplicate.SetActive(true);

            this.canvasUIWeaponBoxes.Add(duplicate);
        }
    }

    public bool ChangeGateKeyCount(int delta)
    {
        int newGateKeyCount = this.gateKeyCount + delta;
        if (newGateKeyCount < 0)
            return false;
        this.gateKeyCount = newGateKeyCount;
        return true;
    }

    public bool ChangeChestKeyCount(int delta)
    {
        int newChestKeyCount = this.chestKeyCount + delta;
        if (newChestKeyCount < 0)
            return false;
        this.gateKeyCount = newChestKeyCount;
        return true;
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
        this.UpdateCanvasElements();
    }

    private int CountAvailableWeapons()
    {
        int count = 0;

        //Debug.Log(this.weapons);

        foreach (Weapon weapon in this.weapons)
        {

            //Debug.Log(weapon);

            if (weapon.GetNumber() > 0)
                ++count;
        }

        return count;
    }

    private void UpdateCanvasElements()
    {
        int position = this.currentWeaponIndex, weaponCount = this.CountAvailableWeapons();

        if (KeyManager.Instance != null)
        {
            if (silverKeyCountText != null)
            {
                silverKeyCountText.text = KeyManager.Instance.CountItem(KeyManager.KeyItem.SilverKey).ToString();
            }
            if (goldenKeyCountText != null)
            {
                goldenKeyCountText.text = KeyManager.Instance.CountItem(KeyManager.KeyItem.GoldKey).ToString();
            }
        }

        for (int i = 0; i < this.numberOfCanvasUIWeaponBoxes; ++i)
        {
            WeaponBoxCanvasUI box = this.canvasUIWeaponBoxes[i].GetComponent<WeaponBoxCanvasUI>();

            if (box != null)
            {
                if (i == 0)
                    box.SetTheBoxChosen();
                else
                    box.SetTheBoxUnchosen();

                if (weaponCount <= 0)
                {
                    box.TurnIntoEmptyBox();
                }
                else
                {
                    this.weapons[position].DisplayInCanvas(box);
                    --weaponCount;
                    position = this.FindNextAvailableWeapon(position);
                }
            }
                
        }
    }

    public bool MoveToTheNextWeaponAsTheCurrentWeapon()
    {
        if (this.currentWeaponIndex < 0)
            return false;
        this.weapons[this.currentWeaponIndex].StopUsing();
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

    public bool ChangeWeaponCount(string weaponName, int number)
    {
        Debug.Log("LET'S EAT PINEAPPLE PIZZA");

        if (number == 0)
            return false;

        Debug.Log("LET'S EAT PINEAPPLE PIZZA!!!");

        if (weaponName == Arrow.GetWeaponName())
        {
            string bowWeaponName = Bow.GetWeaponName();
            
            foreach (Weapon weapon in this.weapons)
                if (weapon.GetNameOfWeapon() == bowWeaponName)
                {
                    string value = weapon.GetWeaponAttributeValue("unusedArrowCount");

                    Debug.Log("PINEAPPLE PIZZA IS READY");

                    if (value == null)
                        return false;

                    int parsedValue = Int32.Parse(value);

                    Debug.Log(parsedValue);

                    return weapon.SetWeaponAttributeValue(
                        "unusedArrowCount",
                        (parsedValue + number).ToString()
                    );
                }

            return false;
        }

        bool result = false;

        Debug.Log("LET'S EAT MORE PINEAPPLE PIZZA!!!");

        int previousCounter = this.CountAvailableWeapons();

        Debug.Log("HELLO???");

        foreach (Weapon weapon in this.weapons)
            if (weapon.GetNameOfWeapon() == weaponName)
            {
                if (number > 0)
                    weapon.IncreaseNumber(number);
                else
                    weapon.DecreaseNumber(-number);
                result = true;
                break;
            }

        if (result)
        {
            int currentCounter = this.CountAvailableWeapons();

            Debug.Log(previousCounter + " " + currentCounter);

            if (previousCounter == 0 && currentCounter > 0)
            {
                this.currentWeaponIndex = this.FindNextAvailableWeapon(0);
                this.weapons[this.currentWeaponIndex].StartUsing();
                Debug.Log("PINEAPPLE PIZZA IS GOOD!!!");
                Debug.Log(this.currentWeaponIndex);
                Debug.Log(this.weapons[0].GetNumber());
                Debug.Log(this.weapons[1].GetNumber());
                Debug.Log(this.weapons[1].gameObject + " " + this.weapons[1].gameObject.activeSelf);
            }
            else if (currentCounter == 0 && previousCounter > 0)
            {
                this.weapons[this.currentWeaponIndex].StopUsing();
                this.currentWeaponIndex = -1;
            }
            else if (currentCounter < previousCounter)
            {
                this.MoveToTheNextWeaponAsTheCurrentWeapon();
            }
        }

        return result;
    }

}
