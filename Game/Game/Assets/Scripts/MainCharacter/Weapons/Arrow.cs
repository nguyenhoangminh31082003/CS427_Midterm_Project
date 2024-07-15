using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Arrow : Weapon
{
    [SerializeField] protected Sprite normalSprite;
    [SerializeField] private Sprite holdingSprite;
    [SerializeField] private Sprite stoppingSprite;
    [SerializeField] private float maximumSpeedX;
    [SerializeField] private float maximumSpeedY;
    [SerializeField] private float MAXIMUM_DAMAGE_CAUSED_PER_HIT;
    [SerializeField] private float NUMBER_OF_MILLISECONDS_OF_MAXIMUM_DURATION_OF_FLYING;
    [SerializeField] private float NUMBER_OF_MILLISECONDS_OF_MAXIMUM_DURATION_OF_STOPPING;
    public enum ArrowState
    {
        NOT_USED_YET,
        PREPARED_TO_BE_USED,
        CURRENTLY_USED,
        STOPPING,
        USED
    }

    private CapsuleCollider2D hittingCollider;
    private ArrowState arrowStatus;
    private float percentage;
    private float startTime;
    private float speedX;
    private float speedY;

    public override void SaveDataToPlayerPrefs()
    {
        string weaponName = this.GetNameOfWeapon();

        PlayerPrefs.SetInt(weaponName + ".number", this.number);
        PlayerPrefs.SetString(weaponName + ".weightPerUnit", this.weightPerUnit.ToString());
        PlayerPrefs.SetString(weaponName + ".currentlyUsed", this.currentlyUsed.ToString());

        PlayerPrefs.SetFloat(weaponName + ".maximumSpeedX", this.maximumSpeedX);
        PlayerPrefs.SetFloat(weaponName + ".maximumSpeedY", this.maximumSpeedY);
        PlayerPrefs.SetFloat(weaponName + ".MAXIMUM_DAMAGE_CAUSED_PER_HIT", this.MAXIMUM_DAMAGE_CAUSED_PER_HIT);
        PlayerPrefs.SetFloat(weaponName + ".NUMBER_OF_MILLISECONDS_OF_MAXIMUM_DURATION_OF_FLYING", this.NUMBER_OF_MILLISECONDS_OF_MAXIMUM_DURATION_OF_FLYING);
        PlayerPrefs.SetFloat(weaponName + ".NUMBER_OF_MILLISECONDS_OF_MAXIMUM_DURATION_OF_STOPPING", this.NUMBER_OF_MILLISECONDS_OF_MAXIMUM_DURATION_OF_STOPPING);

        PlayerPrefs.SetString(weaponName + ".arrowStatus", this.arrowStatus.ToString());
        PlayerPrefs.SetFloat(weaponName + ".percentage", this.percentage);
        PlayerPrefs.SetFloat(weaponName + ".startTime", this.startTime);
        PlayerPrefs.SetFloat(weaponName + ".speedX", this.speedX);
        PlayerPrefs.SetFloat(weaponName + ".speedY", this.speedY);
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

        if (PlayerPrefs.HasKey(weaponName + ".maximumSpeedX"))
            this.maximumSpeedX = PlayerPrefs.GetFloat(weaponName + ".maximumSpeedX");
        
        if (PlayerPrefs.HasKey(weaponName + ".maximumSpeedY"))
            this.maximumSpeedY = PlayerPrefs.GetFloat(weaponName + ".maximumSpeedY");
        
        if (PlayerPrefs.HasKey(weaponName + ".MAXIMUM_DAMAGE_CAUSED_PER_HIT"))
            this.MAXIMUM_DAMAGE_CAUSED_PER_HIT = PlayerPrefs.GetFloat(weaponName + ".MAXIMUM_DAMAGE_CAUSED_PER_HIT");
        
        if (PlayerPrefs.HasKey(weaponName + ".NUMBER_OF_MILLISECONDS_OF_MAXIMUM_DURATION_OF_FLYING"))
            this.NUMBER_OF_MILLISECONDS_OF_MAXIMUM_DURATION_OF_FLYING = PlayerPrefs.GetFloat(weaponName + ".NUMBER_OF_MILLISECONDS_OF_MAXIMUM_DURATION_OF_FLYING");
        
        if (PlayerPrefs.HasKey(weaponName + ".NUMBER_OF_MILLISECONDS_OF_MAXIMUM_DURATION_OF_STOPPING"))
            this.NUMBER_OF_MILLISECONDS_OF_MAXIMUM_DURATION_OF_STOPPING = PlayerPrefs.GetFloat(weaponName + ".NUMBER_OF_MILLISECONDS_OF_MAXIMUM_DURATION_OF_STOPPING");

        if (PlayerPrefs.HasKey(weaponName + ".arrowStatus"))
        {
            string arrowStatus = PlayerPrefs.GetString(weaponName + ".arrowStatus");

            if (arrowStatus == ArrowState.NOT_USED_YET.ToString())
            {
                this.arrowStatus = ArrowState.NOT_USED_YET;
            }
            else if (arrowStatus == ArrowState.PREPARED_TO_BE_USED.ToString())
            {
                this.arrowStatus = ArrowState.PREPARED_TO_BE_USED;
            }
            else if (arrowStatus == ArrowState.CURRENTLY_USED.ToString())
            {
                this.arrowStatus = ArrowState.CURRENTLY_USED;
            }
            else if (arrowStatus == ArrowState.STOPPING.ToString())
            {
                this.arrowStatus = ArrowState.STOPPING;
            }
            else if (arrowStatus == ArrowState.USED.ToString())
            {
                this.arrowStatus = ArrowState.USED;
            }
        }
        
        if (PlayerPrefs.HasKey(weaponName + ".percentage"))
            this.percentage = PlayerPrefs.GetFloat(weaponName + ".percentage");
        
        if (PlayerPrefs.HasKey(weaponName + ".startTime"))
            this.startTime = PlayerPrefs.GetFloat(weaponName + ".startTime");
        
        if (PlayerPrefs.HasKey(weaponName + ".speedX"))
            this.speedX = PlayerPrefs.GetFloat(weaponName + ".speedX");

        if (PlayerPrefs.HasKey(weaponName + ".speedY"))
            this.speedY = PlayerPrefs.GetFloat(weaponName + ".speedY");

        this.partiallyInitialized = true;
    }

    protected override void Start()
    {

        base.Start();

        this.arrowStatus = ArrowState.NOT_USED_YET;

        this.hittingCollider = GetComponent<CapsuleCollider2D>();

        this.hittingCollider.enabled = false;

        this.IncreaseNumber(1);
    }

    public bool IsCurrentlyHeld()
    {
        return this.arrowStatus == ArrowState.PREPARED_TO_BE_USED;
    }

    public bool StartHolding()
    {
        if (this.arrowStatus != ArrowState.NOT_USED_YET)
            return false;
    
        this.startTime = Time.time;
        this.arrowStatus = ArrowState.PREPARED_TO_BE_USED;
        if (this.spriteRenderer != null)
            this.spriteRenderer.sprite = holdingSprite;
        
        return true;
    }

    public float CalculatePercentage()
    {
        if (this.arrowStatus == ArrowState.PREPARED_TO_BE_USED)
        {
            float   currentTime = Time.time,
                    secondsPassed = currentTime - this.startTime;
            this.percentage = 1 / (1 + Mathf.Exp(-secondsPassed));
            return this.percentage;
        }

        if (this.arrowStatus == ArrowState.CURRENTLY_USED)
            return this.percentage;

        return 0;
    }

    public void SetMaximumSpeed(float maximumSpeedX, float maximumSpeedY)
    {
        this.maximumSpeedX = maximumSpeedX;
        this.maximumSpeedY = maximumSpeedY;
    }

    public float GetMaximumSpeedX()
    {
        return this.maximumSpeedX;
    }
    public float GetMaximumSpeedY()
    {
        return this.maximumSpeedY;
    }

    public bool Shot()
    {
        if (this.arrowStatus != ArrowState.PREPARED_TO_BE_USED)
            return false;
        this.hittingCollider.enabled = true;
        this.speedX = this.maximumSpeedX * this.CalculatePercentage();
        this.speedY = this.maximumSpeedY * this.CalculatePercentage();
        this.startTime = Time.time;
        this.arrowStatus = ArrowState.CURRENTLY_USED;
        this.spriteRenderer.sprite = normalSprite;
        return true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
        if (this.arrowStatus == ArrowState.CURRENTLY_USED)
        {
            float   currentTime = Time.time,
                    amountPassed = (currentTime - this.startTime) * 1000;
            if (amountPassed > NUMBER_OF_MILLISECONDS_OF_MAXIMUM_DURATION_OF_FLYING)
            {
                this.speedX = 0;
                this.speedY = 0;
                this.startTime = Time.time;
                this.arrowStatus = ArrowState.STOPPING;
                this.spriteRenderer.sprite = this.stoppingSprite;
                this.hittingCollider.enabled = false;
            }
        }
        else if (this.arrowStatus == ArrowState.STOPPING)
        {
            float   currentTime = Time.time,
                    amountPassed = (currentTime - this.startTime) * 1000;
            if (amountPassed > NUMBER_OF_MILLISECONDS_OF_MAXIMUM_DURATION_OF_STOPPING)
            {
                this.speedX = 0;
                this.speedY = 0;
                this.arrowStatus = ArrowState.USED;
            }
        }
        else if (this.arrowStatus == ArrowState.USED)
        {
            Destroy(this.gameObject);
        }

    }

    protected void FixedUpdate()
    {
        if (this.arrowStatus == ArrowState.CURRENTLY_USED)
        {
            Vector3 currentPosition = this.transform.position;
            currentPosition.x += this.speedX * Time.fixedDeltaTime;
            currentPosition.y += this.speedY * Time.fixedDeltaTime;
            this.transform.position = currentPosition;
        }
    }

    public override double GetAmountDamageThatCanBeCaused()
    {
        return this.CalculatePercentage() * this.MAXIMUM_DAMAGE_CAUSED_PER_HIT;
    }

    public override bool AttackWithConsideringKeyboard()
    {
        return false;
    }

    public ArrowState GetStatus()
    {
        return this.arrowStatus;
    }

    public new static string GetWeaponName()
    {
        return "Arrow";
    }

    public override string GetNameOfWeapon()
    {
        return "Arrow";
    }
}
