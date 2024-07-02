using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    [SerializeField] protected float NUMBER_OF_MILLISECONDS_OF_TIME_OUT_AFTER_ATTACK;
    [SerializeField] protected int unusedArrowCount;
    [SerializeField] protected Arrow sampleArrow;

    private bool attacking;
    private float mostRecentFinishingAttackTime;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        this.spriteRenderer = GetComponent<SpriteRenderer>();

        this.unusedArrowCount = 0;

        this.attacking = false;

        this.unusedArrowCount = 1;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        UpdateSampleArrow();
    }

    private void UpdateSampleArrow()
    {
        if (this.unusedArrowCount > 0)
            this.sampleArrow.StartUsing();
        else
            this.sampleArrow.StopUsing();
    }

    public override bool Attack()
    {
        return false;
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
        return true;
    }

    
}
