using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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
}
