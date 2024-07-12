using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class Sword : Weapon
{

    [SerializeField] private float NUMBER_OF_MILLISECONDS_OF_ATTACK_DURATION;
    [SerializeField] private double damageCausedPerHit;
    [SerializeField] private Sprite movingSwordSprite;
    [SerializeField] private Sprite stillSwordSprite;

    private bool attacking;
    private float attackStartTime;
    private CapsuleCollider2D movingCollider;

    private Quaternion GetDefaultRotation()
    {
        return Quaternion.Euler(0, 0, 90);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        this.spriteRenderer = this.GetComponent<SpriteRenderer>();

        this.movingCollider = this.GetComponent<CapsuleCollider2D>();

        this.movingCollider.enabled = false;

        this.attacking = false;

        this.attackStartTime = Time.time;
    }

    public override bool Attack()
    {
        if (this.attacking || !this.currentlyUsed)
            return false;

        float currentTime = Time.time,
              amountPassed = (currentTime - this.attackStartTime) * 1000;

        if (amountPassed < NUMBER_OF_MILLISECONDS_OF_ATTACK_DURATION * 2)
            return false;

        this.attacking = true;
        this.attackStartTime = Time.time;
        this.spriteRenderer.sprite = this.movingSwordSprite;
        this.movingCollider.enabled = true;
        AudioManager.Instance.PlaySFX("human_atk_sword");
        return true;
    }

    public override bool AttackWithConsideringKeyboard()
    {
        bool spaceEntered = Input.GetKeyDown(KeyCode.Space);
        if (spaceEntered)
        {
            return this.Attack();
        }
        return false;
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
            this.movingCollider.enabled = false;
            this.spriteRenderer.sprite = this.stillSwordSprite;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        UpdateAttack();
    }

    public override double GetAmountDamageThatCanBeCaused()
    {
        return this.damageCausedPerHit;
    }

    public override bool IsBeingUsedToAttack()
    {
        return this.attacking;
    }

    public override void ChangeColorRecursively(Color color)
    {
        base.ChangeColorRecursively(color);
    }

    public new static string GetWeaponName()
    {
        return "Sword";
    }

    public override string GetNameOfWeapon()
    {
        return "Sword";
    }

    public override void DisplayInCanvas(WeaponBoxCanvasUI box)
    {
        box.SetAndShowFirstCounter(this.number);
        box.HideSecondCounter();
        box.SetAndShowWeaponImage(this.stillSwordSprite);
    }
}
