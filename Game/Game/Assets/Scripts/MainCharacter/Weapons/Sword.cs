using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{

    [SerializeField] private float NUMBER_OF_MILLISECONDS_OF_ATTACK_DURATION;
    [SerializeField] private Sprite movingSwordSprite;
    [SerializeField] private Sprite stillWordSprite;

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

        this.attacking = true;
        this.attackStartTime = Time.time;
        this.spriteRenderer.sprite = this.movingSwordSprite;

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
            this.spriteRenderer.sprite = this.stillWordSprite;
            this.transform.rotation = this.GetDefaultRotation();
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        UpdateAttack();
    }
}
