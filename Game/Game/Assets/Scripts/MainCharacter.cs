using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class MainCharacter : MonoBehaviour
{

    public static MainCharacter Instance;

    [SerializeField] private TextMeshProUGUI liveCountText;
    [SerializeField] private TextMeshProUGUI coinCountText;

    public const double NUMBER_OF_MILLISECONDS_OF_INVINCIBILITY_PERIOD = 4000;
    public const double MAXIMUM_WEIGHT_LIMIT = 1E8;
    public const int MAXIMUM_LIVE_COUNT = 5;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody2D;
    private double speedX, speedY;
    private long coinCount;
    [SerializeField] private int liveCount;
    private PlayerBag bag;
    private double weight;

    private bool invincible;
    private float lastDamageTime;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.rigidBody2D = GetComponent<Rigidbody2D>();
        this.bag = GetComponent<PlayerBag>();
        this.speedX = 0;
        this.speedY = 0;
        this.weight = 1;
        this.liveCount = 5;
        this.coinCount = 0;

        this.invincible = false;
        this.lastDamageTime = 0;
    }

    public bool ChangeCoinCount(long change)
    {
        long newCoinCount = this.coinCount + change;
        if (newCoinCount < 0)
        {
            return false;
        }
        this.coinCount = newCoinCount;
        return true;
    }
    /*
    public void ModifySpeed(double speedX, double speedY) {
        this.speedX = speedX;
        this.speedY = speedY;
    }
    */
    public long GetCoinCount() {
        return this.coinCount;
    }

    public int GetLiveConut()
    {
        return this.liveCount;
    }

    public double GetWeight()
    {
        return this.weight;
    }

    public double GetTotalWeight()
    {
        return this.weight + this.bag.GetTotalWeight();
    }

    public Vector2 GetVelocityVector()
    {
        return new Vector2((float)this.speedX, (float)this.speedY);
    } 

    public bool DecreaseLiveCount()
    {
        if (this.invincible)
            return false;
        if (this.liveCount > 0)
        {
            --this.liveCount;
            this.invincible = true;
            this.lastDamageTime = Time.time;
            return true;
        }
        return false;
    }

    public bool IncreaseLiveCount()
    {
        if (this.liveCount < MAXIMUM_LIVE_COUNT)
        {
            ++this.liveCount;
            return true;
        }
        return false;
    }

    void UpdateVelocity()
    {
        bool rightArrow = Input.GetKey(KeyCode.RightArrow),
             leftArrow = Input.GetKey(KeyCode.LeftArrow),
             upArrow = Input.GetKey(KeyCode.UpArrow),
             downArrow = Input.GetKey(KeyCode.DownArrow);
        this.speedX = 0;
        this.speedY = 0;
        if (leftArrow)
            this.speedX -= 1;
        if (rightArrow)
            this.speedX += 1;
        if (upArrow)
            this.speedY += 1;
        if (downArrow)
            this.speedY -= 1;
        if (this.speedX > 0)
            this.spriteRenderer.flipX = false;
        else if (this.speedX < 0)
            this.spriteRenderer.flipX = true;
        double percentage = Math.Max(0, 1 - this.GetTotalWeight() / MAXIMUM_WEIGHT_LIMIT);
        this.speedX *= percentage;
        this.speedY *= percentage;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVelocity();
        UpdateInvincibilityStatus();
        UpdateCanvasElement();
    }

    void UpdateCanvasElement()
    {
        this.liveCountText.text = this.liveCount.ToString();
        this.coinCountText.text = this.coinCount.ToString();
    }

    void UpdateInvincibilityStatus()
    {
        if (this.invincible)
        {
            float currentTime = Time.time,
                  amountPassed = (currentTime - this.lastDamageTime) * 1000;
            if (amountPassed > NUMBER_OF_MILLISECONDS_OF_INVINCIBILITY_PERIOD)
            {
                this.invincible = false;
            } 
            else
            {
                this.spriteRenderer.color = new Color(1f, 1f, 1f, (float)(Math.Sin(amountPassed) + 1) / 2);
            }
        } else
            this.spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    private void FixedUpdate()
    {
        this.rigidBody2D.velocity = new Vector2((float)this.speedX, (float)this.speedY);
    }

    public bool IsDead()
    {
        return this.liveCount <= 0;
    }

    public bool IsAlive()
    {
        return this.liveCount > 0;
    }
}
