using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float chaseRadius;
    private Vector2 originalPosition;
    private bool isChasing = false;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, originalPosition);

        isChasing = distanceToPlayer <= chaseRadius;
    }

    void FixedUpdate() {
        Vector2 direction;
        if (isChasing) {
            direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        }
        else {
            direction = (originalPosition - (Vector2)transform.position).normalized;
        }
        // Move in the correct direction with the set force strength
        rb.MovePosition(rb.position + direction * (speed * Time.fixedDeltaTime));
    }
}
