using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] protected Sprite normalSprite;
    [SerializeField] protected double weightPerUnit;
    [SerializeField] protected bool currentlyUsed;
    [SerializeField] protected int number;

    protected SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.number = 0;

        this.currentlyUsed = false;

        this.gameObject.SetActive(this.currentlyUsed);
    
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();

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

        //Debug.Log(this.gameObject.name + " " + this.currentlyUsed);
        return true;
    }

    public bool StopUsing()
    {
        //Debug.Log(this.gameObject.name + " " + this.currentlyUsed);
        if (!this.currentlyUsed)
            return true;
        if (this.IsBeingUsedToAttack())
            return false;
        this.currentlyUsed = false;
        this.gameObject.SetActive(false);
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
        {
            this.spriteRenderer.enabled = this.currentlyUsed;
        }  
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        UpdateSpriteRenderer();
    }

    public virtual bool Attack()
    {
        return false;
    }

    public virtual double GetAmountDamageThatCanBeCaused()
    {
        return 0;
    }

    public virtual bool IsBeingUsedToAttack()
    {
        return false;
    }

    public virtual void ChangeColorRecursively(Color color)
    {
        if (this.spriteRenderer != null)
            this.spriteRenderer.color = color;
    }

    public virtual bool AttackWithConsideringKeyboard()
    {
        return false;
    }

    public bool IsCurrentlyUsed()
    {
        return this.currentlyUsed;
    }

    public Sprite GetNormalSprite()
    {
        return this.normalSprite;
    }
}
