using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] protected int number;
    [SerializeField] protected bool currentlyUsed;

    protected SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.number = 0;

        this.currentlyUsed = false;

        this.gameObject.SetActive(this.currentlyUsed);
    
        this.spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            spriteRenderer.enabled = this.currentlyUsed;
    }

    public bool StartUsing()
    {
        if (this.number <= 0)
        {
            return false;
        }
        this.currentlyUsed = true;
        this.gameObject.SetActive(true);
        return true;
    }

    public bool IncreaseNumber(int number)
    {
        if (number <= 0)
            return false;
        this.number += number;
        return true;
    }

    public int GetNumber()
    {
        return this.number;
    }

    protected void UpdateSpriteRenderer()
    {
        if (this.spriteRenderer != null)
            this.spriteRenderer.enabled = this.currentlyUsed;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        UpdateSpriteRenderer();
    }

    public void FlipWithVerticalMirror(double x)
    {
        Debug.Log("FLIP!!!");
        Vector3 localScale = transform.localScale;
        if (x < 0)
        {
            localScale.x = Mathf.Abs(localScale.x); // Ensure the sprite is facing right
        }
        else if (x > 0)
        {
            localScale.x = -Mathf.Abs(localScale.x); // Flip the sprite to face left
        }
        transform.localScale = localScale;
    }    
}
