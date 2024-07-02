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

        // Start from the last digit and work backwards
        for (int i = length - 1; i >= 0; i--)
        {
            if (digits[i] < '0' || digits[i] > '9')
            {
                throw new ArgumentException("Input must be a valid non-negative number", nameof(number));
            }

            if (digits[i] < '9')
            {
                digits[i]++;
                this.arrowIndex = new string(digits);
                return;
            }

            digits[i] = '0'; // Set current digit to '0' and carry the increment to the next digit
        }

        // If we reach here, it means all digits were '9'
        // We need to add a '1' at the beginning
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
        //Debug.Log("One");

        if (this.unusedArrowCount > 0 &&
            !this.attacking)
        {
            this.sampleArrow.StartUsing();
            return;
        }

        //Debug.Log(this.sampleArrow.IsCurrentlyUsed());
        //Debug.Log("Two");

        this.sampleArrow.StopUsing();
    }

    private void UpdateCurrentlyHeldArrow()
    {
        if (this.currentlyHeldArrow == null)
            return;

        Arrow arrow = this.currentlyHeldArrow.GetComponent<Arrow>();

        //Debug.Log("----------------");

        //Debug.Log(arrow.GetStatus());
        //Debug.Log(this.currentlyHeldArrow.name + " " + arrow.IsCurrentlyHeld());
        //Debug.Log(this.currentlyHeldArrow.name + " " + arrow.IsCurrentlyUsed());

        if (!arrow.IsCurrentlyUsed())
        {
            arrow.StartUsing();
            arrow.StartHolding();
        }

        //Debug.Log(arrow.GetStatus());
        //Debug.Log(this.currentlyHeldArrow.name + " " + arrow.IsCurrentlyHeld());
        //Debug.Log(this.currentlyHeldArrow.name + " " + arrow.IsCurrentlyUsed());

        if (arrow.IsCurrentlyHeld())
        {
            this.spriteRenderer.color = arrow.GetCurrentColor();
        }
        else
        {
            this.spriteRenderer.color = new Color(1, 1, 1, 1);
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

        //Debug.Log(arrow.IsCurrentlyHeld());

        //Debug.Log(this.currentlyHeldArrow.name + "-" + this.currentlyHeldArrow.GetComponent<Arrow>().IsCurrentlyHeld());

        return true;
    }


}
