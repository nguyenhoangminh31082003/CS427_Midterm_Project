using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{

    [SerializeField] private float NUMBER_OF_MILLISECONDS_OF_ATTACK_DURATION;

    bool attacking;
    float attackStartTime;

    private Quaternion GetDefaultRotation()
    {
        return Quaternion.Euler(0, 0, 90);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        this.spriteRenderer = GetComponent<SpriteRenderer>();

        this.attacking = false;
    }

    public override bool Attack()
    {
        if (this.attacking || !this.currentlyUsed)
            return false;

        //Debug.Log("Attack started successfully!!!");

        this.attacking = true;
        this.attackStartTime = Time.time;

        return true;
    }

    private void UpdateAttack()
    {
        if (!this.attacking || !this.currentlyUsed)
            return;
        float currentTime = Time.time,
              amountPassed = (currentTime - this.attackStartTime) * 1000;
        if (amountPassed > NUMBER_OF_MILLISECONDS_OF_ATTACK_DURATION)
        {
            this.attacking = false;
            this.transform.rotation = this.GetDefaultRotation();
        }
        else
        {
            float angle = 180 * amountPassed / NUMBER_OF_MILLISECONDS_OF_ATTACK_DURATION;
            Debug.Log(angle);
            this.transform.rotation = Quaternion.Euler(0, 0, 90 - angle);
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        UpdateAttack();
    }
}
