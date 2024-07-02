using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] protected double weightPerUnit;
    [SerializeField] protected bool currentlyUsed;
    [SerializeField] protected int number;

    protected GameManager gameManager;

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

        this.gameManager = GameManager.Instance;
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
        {
            this.spriteRenderer.enabled = this.currentlyUsed;
        }  
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //Debug.Log("Weapon UPDATE!!!");
        UpdateSpriteRenderer();
    }

    public virtual bool Attack()
    {
        return false;
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        gameManager.CollisionHandler(other.transform.tag, other.transform.name, this.tag, this.name);   
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
        this.spriteRenderer.color = color;
    }

}
