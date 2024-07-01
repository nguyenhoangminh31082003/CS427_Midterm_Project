using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementBase : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 2f;
    protected MainCharacter player;
    protected EnemyController enemyController;
    protected Rigidbody2D rb;
    protected Knockback knockback;

    protected SpriteRenderer sr;

    protected Vector2 moveDir;
    protected virtual void Awake()
    {
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        enemyController = GetComponent<EnemyController>();

        player = MainCharacter.Instance;
    }
    protected virtual void Start() {}
    protected virtual void Update() {}
    protected virtual void FixedUpdate() {
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
