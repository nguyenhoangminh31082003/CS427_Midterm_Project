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

    protected GameManager gameManager;

    protected SpriteRenderer spriteRenderer;

    protected bool partiallyInitialized = false;

    public virtual double FindTotalWeight()
    {
        return this.weightPerUnit * this.number; 
    }
    public virtual void SetDefaultValuesToPlayerPrefs()
    {
        string weaponName = this.GetNameOfWeapon();

        PlayerPrefs.SetInt(weaponName + ".number", 0);
        PlayerPrefs.SetString(weaponName + ".weightPerUnit", "0");
        PlayerPrefs.SetString(weaponName + ".currentlyUsed", false.ToString());
    }

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

        this.partiallyInitialized = true;
    }

    protected virtual void Start()
    {

        if (!this.partiallyInitialized)
        {
            this.number = 0;

            this.currentlyUsed = false;
        }

        this.gameObject.SetActive(this.currentlyUsed);
    
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            spriteRenderer.enabled = this.currentlyUsed;

        this.gameManager = GameManager.Instance;
    }

    public bool StartUsing()
    {
        if (this.number <= 0)
        {
            return false;
        }

        this.currentlyUsed = true;
        this.gameObject.SetActive(true);

        return true;
    }

    public bool StopUsing()
    {
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
    protected virtual void Update()
    {
        UpdateSpriteRenderer();
    }

    public virtual bool Attack()
    {
        return false;
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        gameManager.CollisionHandler(other.transform.tag, other.transform.name, this.tag, this.name);   
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

    public virtual bool ChangeAmountDamageThatCanBeCaused(double newAmount)
    {
        return false;
    }
}
