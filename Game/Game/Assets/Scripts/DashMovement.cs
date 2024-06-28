using System.Collections;
using UnityEngine;

public class DashMovement : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed;
    public float dashPower;

    private float dashingTime = 1f;

    private float dashingCooldown = 2f;
    public float dashRadius;

    private Rigidbody2D rb;

    public bool isDashing = false;

    private bool canDash = true;

    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDashing)
            return;

        direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= dashRadius && canDash){
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate() {
        if (isDashing) {
            return;
        }
        rb.velocity = direction * moveSpeed;
    }

    IEnumerator Dash() {
        canDash = false;
        isDashing = true;
        rb.velocity = direction * dashPower;
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            // Deal damage to the player
            print("damage");
        }
    }
}