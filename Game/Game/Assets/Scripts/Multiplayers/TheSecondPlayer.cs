using TMPro;
using System;
using UnityEngine;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;

public class TheSecondPlayer : ThePlayer
{
    public static TheSecondPlayer Instance;

    protected override void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    protected override void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.rigidBody2D = this.GetComponent<Rigidbody2D>();

        if (this.bag == null)
            this.bag = this.playerBag.GetComponent<ThePlayerBag>();

        this.knockback = this.GetComponent<Knockback>();

        if (!this.partialInitialized)
        {
            this.liveCount = 5;
            this.coinCount = 0;
            this.speedX = 0;
            this.speedY = 0;
            this.weight = 1;

            this.invincible = false;
            this.lastDamageTime = 0;
        }

        this.theGameManager = TheGameManager.Instance;
        this.dialogueManager = DialogueManager.Instance;
    }

    protected override void UpdateVelocity()
    {
        bool rightArrow     = Input.GetKey(KeyCode.RightArrow),
             leftArrow      = Input.GetKey(KeyCode.LeftArrow),
             upArrow        = Input.GetKey(KeyCode.UpArrow),
             downArrow      = Input.GetKey(KeyCode.DownArrow);

        this.speedX = 0;
        this.speedY = 0;

        if (leftArrow)
            this.speedX -= DEFAULT_SPEED;

        if (rightArrow)
            this.speedX += DEFAULT_SPEED;

        if (upArrow)
            this.speedY += DEFAULT_SPEED;

        if (downArrow)
            this.speedY -= DEFAULT_SPEED;

        if (this.speedX > 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (this.speedX < 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }

        double percentage = Math.Max(0, 1 - this.GetTotalWeight() / MAXIMUM_WEIGHT_LIMIT);

        this.speedX *= percentage;
        this.speedY *= percentage;
    }

    protected override void UpdateKeyUsage()
    {
        if (Input.GetKey(KeyCode.Keypad2))
        {
            //WHERE IS THE MANAGER!!!
        }
    }

    protected override void UpdateCurrentlyUsedWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            this.bag.MoveToTheNextWeaponAsTheCurrentWeapon();
        }
    }

}
