using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainCharacter : MonoBehaviour
{

    public const double MAXIMUM_WEIGHT_LIMIT = 1E8;
    public const int MAXIMUM_LIVE_COUNT = 5;

    private SpriteRenderer spriteRender;
    private Rigidbody2D rigidBody2D;
    private double speedX, speedY;
    private long coinCount;
    private double weight;
    private int liveCount;

    // Start is called before the first frame update
    void Start()
    {
        this.spriteRender = GetComponent<SpriteRenderer>();
        this.rigidBody2D = GetComponent<Rigidbody2D>();
        this.weight = 1;
        this.speedX = 0;
        this.speedY = 0;
        this.liveCount = 5;
        this.coinCount = 0;
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

    public void ModifySpeed(double speedX, double speedY) {
        this.speedX = speedX;
        this.speedY = speedY;
    }

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

    public Vector2 GetVelocityVector()
    {
        return new Vector2((float)this.speedX, (float)this.speedY);
    } 

    public bool DecreaseLiveCount()
    {
        if (this.liveCount > 0)
        {
            --this.liveCount; 
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
        double percentage = 1 - weight / MAXIMUM_WEIGHT_LIMIT;
        this.speedX *= percentage;
        this.speedY *= percentage;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVelocity();
    }

    private void FixedUpdate()
    {
        //GameManager.setPlayerVelocity(this.speedX, this.speedY);
        this.rigidBody2D.velocity = new Vector2((float)this.speedX, (float)this.speedY);
    }
}
