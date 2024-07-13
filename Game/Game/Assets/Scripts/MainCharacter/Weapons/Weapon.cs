using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{

    [SerializeField] protected double weightPerUnit;
    [SerializeField] protected bool currentlyUsed;
    [SerializeField] protected int number;

    protected SpriteRenderer spriteRenderer;

    public virtual void SaveDataToPlayerPrefs()
    {
        string weaponName = this.GetNameOfWeapon();

        PlayerPrefs.SetInt(weaponName + ".number", this.number);
        PlayerPrefs.SetString(weaponName + ".weightPerUnit", this.weightPerUnit.ToString());
        PlayerPrefs.SetString(weaponName + ".currentlyUsed", this.currentlyUsed.ToString());
    }

    public virtual void LoadDataFromPlayerPrefs()
    {
        string weaponName = this.GetNameOfWeapon();

        this.number = PlayerPrefs.GetInt(weaponName + ".number");
        this.weightPerUnit = double.Parse(PlayerPrefs.GetString(weaponName + ".weightPerUnit"));
        this.currentlyUsed = bool.Parse(PlayerPrefs.GetString(weaponName + ".currentlyUsed"));
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.number = 0;

        this.currentlyUsed = false;

        this.gameObject.SetActive(this.currentlyUsed);
    
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            spriteRenderer.enabled = this.currentlyUsed;
    }

    public bool StartUsing()
    {
        if (this.number <= 0)
        {
            return false;
        }
        this.currentlyUsed = true;
        this.gameObject.SetActive(true);

        //Debug.Log(this.gameObject.name + " " + this.currentlyUsed);
        return true;
    }

    public bool StopUsing()
    {
        //Debug.Log(this.gameObject.name + " " + this.currentlyUsed);
        if (!this.currentlyUsed)
            return true;
        if (this.IsBeingUsedToAttack())
            return false;
        this.currentlyUsed = false;
        this.gameObject.SetActive(false);
        return true;
    }

    public bool IncreaseNumber(int number)
    {
        if (number <= 0)
            return false;
        this.number += number;
        return true;
    }

    public bool DecreaseNumber(int number)
    {
        if (number <= 0 || this.number < number)
            return false;
        this.number -= number;
        return true;
    }

    public int GetNumber()
    {
        return this.number;
    }

    protected void UpdateSpriteRenderer()
    {
        if (this.spriteRenderer != null)
        {
            this.spriteRenderer.enabled = this.currentlyUsed;
        }  
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        UpdateSpriteRenderer();
    }

    public virtual bool Attack()
    {
        return false;
    }

    public virtual double GetAmountDamageThatCanBeCaused()
    {
        return 0;
    }

    public virtual bool IsBeingUsedToAttack()
    {
        return false;
    }

    public virtual void ChangeColorRecursively(Color color)
    {
        if (this.spriteRenderer != null)
            this.spriteRenderer.color = color;
    }

    public virtual bool AttackWithConsideringKeyboard()
    {
        return false;
    }

    public bool IsCurrentlyUsed()
    {
        return this.currentlyUsed;
    }

    public virtual void DisplayInCanvas(WeaponBoxCanvasUI box)
    {
    }

    public static string GetWeaponName()
    {
        return "Weapon";
    }

    public virtual string GetNameOfWeapon()
    {
        return "Weapon";
    }

    public virtual string GetWeaponAttributeValue(string attributeName)
    {
        /*
         
            This function should be used with high caution
        
        */
        return null;
    }

    public virtual bool SetWeaponAttributeValue(string attributeName, string value)
    {
        /*
         
            This function should be used with high caution
        
        */
        return false;
    }

}
