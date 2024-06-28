using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovement : MonoBehaviour
{
    [SerializeField] public Transform[] patrolPoints;
    public float speed;
    private int targetPoint = 0;
    private SpriteRenderer sr;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start() {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {            
        // Check if the enemy has reached the current waypoint
        if (Vector2.Distance(transform.position, patrolPoints[targetPoint].position) < 0.1f)
        {
            targetPoint = (targetPoint + 1) % patrolPoints.Length;
            FaceWaypoint(patrolPoints[targetPoint]);
        }
    }

    void FixedUpdate() {
        Vector2 direction = ((Vector2)patrolPoints[targetPoint].position - (Vector2)transform.position).normalized;
        // Move in the correct direction with the set force strength
        rb.MovePosition(rb.position + direction * (speed * Time.fixedDeltaTime));
    }
    void FaceWaypoint(Transform waypoint)
    {
        if (waypoint != null)
        {
            if (transform.position.x < waypoint.position.x)
            {
                // Waypoint is to the left
                sr.flipX = false; // Flip to face left
            }
            else if (transform.position.x > waypoint.position.x)
            {
                // Waypoint is to the right
                sr.flipX = true; // Flip to face left
            }
        }
    }
}
