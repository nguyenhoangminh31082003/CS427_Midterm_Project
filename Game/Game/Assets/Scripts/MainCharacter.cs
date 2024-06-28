using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainCharacter : MonoBehaviour
{

    const double WEIGHT_LIMIT = 1E8;

    private SpriteRenderer spriteRender;
    private Rigidbody2D rigidBody2D;
    private double speedX, speedY;
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
            this.speedX += 1;
        if (rightArrow)
            this.speedX -= 1;
        if (upArrow)
            this.speedY += 1;
        if (downArrow)
            this.speedY -= 1;
        double percentage = 1 - weight / WEIGHT_LIMIT;
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
