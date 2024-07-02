using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    [SerializeField] protected int unusedArrowCount;
    [SerializeField] protected Arrow sampleArrow;
    

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        this.spriteRenderer = GetComponent<SpriteRenderer>();

        this.unusedArrowCount = 0;

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
        return false;
    }

    public override void ChangeColorRecursively(Color color)
    {
        base.ChangeColorRecursively(color);
    }
}
