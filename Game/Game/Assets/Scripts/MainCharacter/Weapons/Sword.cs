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

    public override void SetDefaultValuesToPlayerPrefs()
    {
        string weaponName = this.GetNameOfWeapon();

        PlayerPrefs.SetInt(weaponName + ".number", 0);
        PlayerPrefs.SetString(weaponName + ".weightPerUnit", "0");
        PlayerPrefs.SetString(weaponName + ".currentlyUsed", false.ToString());

        PlayerPrefs.SetFloat(weaponName + ".NUMBER_OF_MILLISECONDS_OF_ATTACK_DURATION", 200);
        PlayerPrefs.SetString(weaponName + ".damageCausedPerHit", "3");
        PlayerPrefs.SetString(weaponName + ".attacking", false.ToString());
        PlayerPrefs.SetFloat(weaponName + ".attackStartTime", 0);
    }
    public override void SaveDataToPlayerPrefs()
    {
        string weaponName = this.GetNameOfWeapon();

        PlayerPrefs.SetInt(weaponName + ".number", this.number);
        PlayerPrefs.SetString(weaponName + ".weightPerUnit", this.weightPerUnit.ToString());
        PlayerPrefs.SetString(weaponName + ".currentlyUsed", this.currentlyUsed.ToString());

        PlayerPrefs.SetFloat(weaponName + ".NUMBER_OF_MILLISECONDS_OF_ATTACK_DURATION", this.NUMBER_OF_MILLISECONDS_OF_ATTACK_DURATION);
        PlayerPrefs.SetString(weaponName + ".damageCausedPerHit", this.damageCausedPerHit.ToString());
        PlayerPrefs.SetString(weaponName + ".attacking", this.attacking.ToString());
        PlayerPrefs.SetFloat(weaponName + ".attackStartTime", this.attackStartTime);
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

        if (PlayerPrefs.HasKey(weaponName + ".NUMBER_OF_MILLISECONDS_OF_ATTACK_DURATION"))
            this.NUMBER_OF_MILLISECONDS_OF_ATTACK_DURATION = PlayerPrefs.GetFloat(weaponName + ".NUMBER_OF_MILLISECONDS_OF_ATTACK_DURATION");
     
        if (PlayerPrefs.HasKey(weaponName + ".damageCausedPerHit"))
            this.damageCausedPerHit = double.Parse(PlayerPrefs.GetString(weaponName + ".damageCausedPerHit"));

        if (PlayerPrefs.HasKey(weaponName + ".attacking"))
            this.attacking = bool.Parse(PlayerPrefs.GetString(weaponName + ".attacking"));
        
        if (PlayerPrefs.HasKey(weaponName + ".attackStartTime"))
            this.attackStartTime = PlayerPrefs.GetFloat(weaponName + ".attackStartTime");

        this.partiallyInitialized = true;
    }

    private Quaternion GetDefaultRotation()
    {
        return Quaternion.Euler(0, 0, 90);
    }

    protected override void Start()
    {
        base.Start();

        this.spriteRenderer = this.GetComponent<SpriteRenderer>();

        this.movingCollider = this.GetComponent<CapsuleCollider2D>();

        if (!this.partiallyInitialized)
        {
            this.attacking = false;

            this.attackStartTime = 0;
        }

        this.movingCollider.enabled = false;
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

    protected override void Update()
    {
        base.Update();

        this.UpdateAttack();
    }

    public override double GetAmountDamageThatCanBeCaused()
    {
        return this.damageCausedPerHit;
    }

    public override bool ChangeAmountDamageThatCanBeCaused(double newAmount)
    {

        if (newAmount <= 0)
            return false;

        this.damageCausedPerHit = newAmount;

        return true;
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

    public override double FindTotalWeight()
    {
        return base.FindTotalWeight();
    }
}
