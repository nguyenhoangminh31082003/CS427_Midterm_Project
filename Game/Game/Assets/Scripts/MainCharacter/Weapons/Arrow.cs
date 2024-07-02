using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Weapon
{
    [SerializeField] private Sprite normalSprite;
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

    // Start is called before the first frame update
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
            Vector3 currentPosition = transform.position;
            currentPosition.x += this.speedX * Time.fixedDeltaTime;
            currentPosition.y += this.speedY * Time.fixedDeltaTime;
            transform.position = currentPosition;

            //Debug.Log(this.speedX + " " + this.speedY);
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
}
