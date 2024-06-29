using System.Collections;
using UnityEngine;

public abstract class MovementBase : MonoBehaviour
{
    public GameObject player;
    protected Rigidbody2D rb;
    protected Knockback knockback;

    protected SpriteRenderer sr;

    protected virtual void Awake()
    {
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    protected abstract void Update();
    protected abstract void FixedUpdate();
}
