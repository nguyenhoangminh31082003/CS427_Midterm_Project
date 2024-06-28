using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{

    private SpriteRenderer spriteRender;
    private Rigidbody2D rigidBody2D;
    private float speedX, speedY;
    private float weight;

    // Start is called before the first frame update
    void Start()
    {
        this.spriteRender = GetComponent<SpriteRenderer>();
        this.rigidBody2D = GetComponent<Rigidbody2D>();
        this.weight = 1;
        this.speedX = 0;
        this.speedY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        bool rightArrow = Input.GetKey(KeyCode.RightArrow),
             leftArrow = Input.GetKey(KeyCode.LeftArrow),
             upArrow   = Input.GetKey(KeyCode.UpArrow),
             downArrow = Input.GetKey(KeyCode.DownArrow);
        if (leftArrow && !(rightArrow || upArrow || downArrow))
        {
            this.speedX = -1;
            this.speedY = 0;
        } 
        else if (rightArrow && !(leftArrow || upArrow || downArrow))
        {
            this.speedX = 1;
            this.speedY = 0;
        } 
        else if (upArrow && !(leftArrow || rightArrow || downArrow)) 
        {
            this.speedX = 0;
            this.speedY = 1;
        }
        else if (downArrow && !(leftArrow || rightArrow || upArrow))
        {
            this.speedX = 0;
            this.speedY = -1;
        }
        else
        {
            this.speedX = 0;
            this.speedY = 0;
        }
    }

    private void FixedUpdate()
    {
        this.rigidBody2D.velocity = new Vector2(this.speedX, this.speedY);
    }
}
