using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    [SerializeField] protected float NUMBER_OF_MILLISECONDS_OF_TIME_OUT_AFTER_ATTACK;
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

        this.unusedArrowCount = 1;
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

    public bool StartHolding()
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
        
        Arrow arrow = this.currentlyHeldArrow.GetComponent<Arrow>();

        arrow.StartUsing();

        arrow.StartHolding();

        return true;
    }


}
