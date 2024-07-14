using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class Bow : Weapon
{
    [SerializeField] protected float NUMBER_OF_MILLISECONDS_OF_TIME_OUT_AFTER_ATTACK;
    [SerializeField] protected Sprite stillBowSprite;
    [SerializeField] protected int unusedArrowCount;
    [SerializeField] protected Arrow sampleArrow;

    private float mostRecentFinishingAttackTime;
    private GameObject currentlyHeldArrow;
    private string arrowIndex;
    private bool attacking;

    public override void SaveDataToPlayerPrefs()
    {
        string weaponName = this.GetNameOfWeapon();

        PlayerPrefs.SetInt(weaponName + ".number", this.number);
        PlayerPrefs.SetString(weaponName + ".weightPerUnit", this.weightPerUnit.ToString());
        PlayerPrefs.SetString(weaponName + ".currentlyUsed", this.currentlyUsed.ToString());

        PlayerPrefs.SetFloat(weaponName + ".NUMBER_OF_MILLISECONDS_OF_TIME_OUT_AFTER_ATTACK", this.NUMBER_OF_MILLISECONDS_OF_TIME_OUT_AFTER_ATTACK);
        PlayerPrefs.SetInt(weaponName + ".unusedArrowCount", this.unusedArrowCount);
        PlayerPrefs.SetFloat(weaponName + ".mostRecentFinishingAttackTime", this.mostRecentFinishingAttackTime);
        PlayerPrefs.SetString(weaponName + ".arrowIndex", this.arrowIndex);
        PlayerPrefs.SetString(weaponName + ".attacking", this.attacking.ToString());
    }

    public override void LoadDataFromPlayerPrefs()
    {
        string weaponName = this.GetNameOfWeapon();

        if (PlayerPrefs.HasKey(weaponName + ".number"))
           this.number = PlayerPrefs.GetInt(weaponName + ".number");
    
        if (PlayerPrefs.HasKey(weaponName + ".weightPerUnit"))
            this.weightPerUnit = double.Parse(PlayerPrefs.GetString(weaponName + ".weightPerUnit"));
        
        if (PlayerPrefs.HasKey(weaponName + ".currentlyUsed"))
            this.currentlyUsed = bool.Parse(PlayerPrefs.GetString(weaponName + ".currentlyUsed"));

        if (PlayerPrefs.HasKey(weaponName + ".NUMBER_OF_MILLISECONDS_OF_TIME_OUT_AFTER_ATTACK"))
            this.NUMBER_OF_MILLISECONDS_OF_TIME_OUT_AFTER_ATTACK = PlayerPrefs.GetFloat(weaponName + ".NUMBER_OF_MILLISECONDS_OF_TIME_OUT_AFTER_ATTACK");
        
        if (PlayerPrefs.HasKey(weaponName + ".unusedArrowCount"))
            this.unusedArrowCount = PlayerPrefs.GetInt(weaponName + ".unusedArrowCount");
        
        if (PlayerPrefs.HasKey(weaponName + ".mostRecentFinishingAttackTime"))
            this.mostRecentFinishingAttackTime = PlayerPrefs.GetFloat(weaponName + ".mostRecentFinishingAttackTime");
        
        if (PlayerPrefs.HasKey(weaponName + ".arrowIndex"))
            this.arrowIndex = PlayerPrefs.GetString(weaponName + ".arrowIndex");

        if (PlayerPrefs.HasKey(weaponName + ".attacking"))
            this.attacking = bool.Parse(PlayerPrefs.GetString(weaponName + ".attacking"));
    }

    protected override void Start()
    {
        base.Start();

        this.spriteRenderer = GetComponent<SpriteRenderer>();

        this.unusedArrowCount = 0;

        this.attacking = false;

        this.arrowIndex = "0";

        this.currentlyHeldArrow = null;
    }

    private void IncreaseArrowIndexByOne()
    {
        char[] digits = this.arrowIndex.ToCharArray();
        int length = digits.Length;

        for (int i = length - 1; i >= 0; i--)
        {

            if (digits[i] < '9')
            {
                digits[i]++;
                this.arrowIndex = new string(digits);
                return;
            }

            digits[i] = '0';
        }

        this.arrowIndex = '1' + new string(digits);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        this.UpdateSampleArrow();
    
        this.UpdateCurrentlyHeldArrow();
    }

    private void UpdateSampleArrow()
    {

        if (this.unusedArrowCount > 0 &&
            !this.attacking)
        {
            this.sampleArrow.StartUsing();
            return;
        }

        this.sampleArrow.StopUsing();
    }

    private void UpdateCurrentlyHeldArrow()
    {
        if (this.currentlyHeldArrow == null)
            return;

        Arrow arrow = this.currentlyHeldArrow.GetComponent<Arrow>();

        if (!arrow.IsCurrentlyUsed())
        {
            arrow.StartUsing();
            arrow.StartHolding();
        }
    }

    public override bool Attack()
    {
        return false;
    }

    public override bool AttackWithConsideringKeyboard()
    {
        bool    spaceEntered    = Input.GetKeyDown(KeyCode.Space),
                spaceHeld       = Input.GetKey(KeyCode.Space),
                spaceReleased   = Input.GetKeyUp(KeyCode.Space),
                result          = false;

        if (spaceEntered)
        {
            result = result || this.StartHolding();
        }

        if (spaceHeld)
        {

        }

        if (spaceReleased)
        {
            result = result || this.Shot();
            AudioManager.Instance.PlaySFX("human_atk_arrow");
        }

        return result;
    }

    public override double GetAmountDamageThatCanBeCaused()
    {
        return 0;
    }

    public override bool IsBeingUsedToAttack()
    {
        return this.attacking;
    }

    public override void ChangeColorRecursively(Color color)
    {
        base.ChangeColorRecursively(color);

        this.sampleArrow.ChangeColorRecursively(color);
    }

    private bool StartHolding()
    {
        if (this.attacking ||
            this.unusedArrowCount <= 0)
            return false;
        float   currentTime = Time.time,
                amountPassed = (currentTime - this.mostRecentFinishingAttackTime) * 1000;
        if (amountPassed < NUMBER_OF_MILLISECONDS_OF_TIME_OUT_AFTER_ATTACK)
            return false;
        this.currentlyHeldArrow = Instantiate(this.sampleArrow.gameObject, this.transform);
        this.currentlyHeldArrow.name = "Arrow " + this.arrowIndex;
        this.IncreaseArrowIndexByOne();
        this.attacking = true;
        --this.unusedArrowCount;

        Arrow arrow = this.currentlyHeldArrow.GetComponent<Arrow>();

        arrow.StartUsing();

        arrow.StartHolding();

        return true;
    }

    private bool Shot()
    {
        if (this.currentlyHeldArrow == null)
            return false;

        this.currentlyHeldArrow.transform.SetParent(null);

        Arrow arrow = this.currentlyHeldArrow.GetComponent<Arrow>();

        if (this.currentlyHeldArrow.transform.localScale.x < 0)
            arrow.SetMaximumSpeed(-arrow.GetMaximumSpeedX(), arrow.GetMaximumSpeedY());

        arrow.Shot();

        this.currentlyHeldArrow = null;
        this.attacking = false;

        return true;
    }

    public new static string GetWeaponName()
    {
        return "Bow";
    }

    public override string GetNameOfWeapon()
    {
        return "Bow";
    }

    public override void DisplayInCanvas(WeaponBoxCanvasUI box)
    {
        box.SetAndShowFirstCounter(this.number);
        box.SetAndShowSecondCounter(this.unusedArrowCount);
        box.SetAndShowWeaponImage(this.stillBowSprite);
    }

    public override string GetWeaponAttributeValue(string attributeName)
    {

        if (attributeName == "unusedArrowCount")
            return this.unusedArrowCount.ToString();

        return null;
    }

    public override bool SetWeaponAttributeValue(string attributeName, string value)
    {
        
        if (attributeName == "unusedArrowCount")
        {
            int parsedValue = Int32.Parse(value);
            if (parsedValue < 0)
                return false;

            this.unusedArrowCount = parsedValue;

            return true;
        }

        return false;
    }
}
