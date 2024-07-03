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

    private bool attacking;
    private GameObject currentlyHeldArrow;
    private float mostRecentFinishingAttackTime;
    private string arrowIndex;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        this.spriteRenderer = GetComponent<SpriteRenderer>();

        this.unusedArrowCount = 0;

        this.attacking = false;

        this.arrowIndex = "0";

        this.currentlyHeldArrow = null;

        this.unusedArrowCount = 5;
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

    public override void DisplayInCanvas(GameObject container)
    {
        /*
        GameObject weaponImage = new GameObject(this.GetWeaponName() + "_Image");
        weaponImage.transform.parent = container.transform;
        Image image = weaponImage.AddComponent<Image>();
        image.sprite = this.stillBowSprite;
        */
        GameObject weaponImage = Instantiate(container, container.transform);
        weaponImage.name = this.GetWeaponName() + "_Image";
        weaponImage.transform.localPosition = new Vector2(0, 0);
        Image image = weaponImage.GetComponent<Image>();
        image.sprite = this.stillBowSprite;
    }

    public override string GetWeaponName()
    {
        return "Bow";
    }
}
