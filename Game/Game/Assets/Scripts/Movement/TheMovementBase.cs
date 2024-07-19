using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TheMovementBase : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 2f;
    protected TheFirstPlayer theFirstPlayer;
    protected TheEnemyController theEnemyController;
    protected Rigidbody2D rb;
    protected Knockback knockback;

    protected SpriteRenderer sr;

    protected Animator _animator;

    private Vector2 moveDir;
    protected virtual void Awake()
    {
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        theEnemyController = GetComponent<TheEnemyController>();
        
    }
    protected virtual void Start() {
        this.theFirstPlayer = TheFirstPlayer.Instance;
    }

    protected virtual void Update() {}
        
    protected virtual void FixedUpdate() {    
        if (DialogueManager.Instance != null)
        {
            if (DialogueManager.Instance.isDialogueActive) { 
                return; 
            }
        }

        if (knockback.gettingKnockedBack) { return; }

        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

        if (moveDir.x < -0.1f) {
            sr.flipX = true;
        } else if (moveDir.x > 0.1f) {
            sr.flipX = false;
        }
    }
    public void MoveTo(Vector2 targetPosition) {
        moveDir = targetPosition;
    }
    public void StopMoving() {
        moveDir = Vector3.zero;
    }

    public abstract void Roaming();

    public abstract void Attacking();
}
