using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TheMovementBase : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 2f;
    protected TheFirstPlayer theFirstPlayer;
    protected TheSecondPlayer theSecondPlayer;
    protected TheEnemyController theEnemyController;
    protected Rigidbody2D rb;
    protected Knockback knockback;

    protected SpriteRenderer sr;

    protected Animator _animator;

    private Vector2 moveDirection;
    protected virtual void Awake()
    {
        this.knockback = GetComponent<Knockback>();
        this.rb = GetComponent<Rigidbody2D>();
        this.sr = GetComponent<SpriteRenderer>();
        this._animator = GetComponent<Animator>();
        this.theEnemyController = GetComponent<TheEnemyController>();
        
    }
    protected virtual void Start() 
    {
        this.theFirstPlayer = TheFirstPlayer.Instance;
        this.theSecondPlayer = TheSecondPlayer.Instance;
    }

    protected virtual void Update() {}
        
    protected virtual void FixedUpdate() 
    {    
        if (DialogueManager.Instance != null)
        {
            if (DialogueManager.Instance.isDialogueActive) 
            { 
                return; 
            }
        }

        if (knockback.gettingKnockedBack) 
        { 
            return; 
        }

        rb.MovePosition(rb.position + this.moveDirection * (moveSpeed * Time.fixedDeltaTime));

        if (this.moveDirection.x < -0.1f) {
            sr.flipX = true;
        } else if (this.moveDirection.x > 0.1f) {
            sr.flipX = false;
        }
    }
    public void MoveTo(Vector2 targetPosition) 
    {
        this.moveDirection = targetPosition;
    }
    public void StopMoving() {
        this.moveDirection = Vector3.zero;
    }

    public abstract void Roaming();

    public abstract void Attacking();
}
