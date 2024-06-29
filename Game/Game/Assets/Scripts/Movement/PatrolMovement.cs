using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovement : MovementBase
{
    [SerializeField] public Transform[] patrolPoints;
    public float speed;
    private int targetPoint;
    private Vector2 direction;

    void Start() {
        targetPoint = 0;
    }
    // Update is called once per frame
    protected override void Update()
    {            
        // Check if the enemy has reached the current waypoint
        if (Vector2.Distance(transform.position, patrolPoints[targetPoint].position) < 0.1f)
        {
            targetPoint = (targetPoint + 1) % patrolPoints.Length;
            FaceWaypoint(patrolPoints[targetPoint]);
        }
        direction = ((Vector2)patrolPoints[targetPoint].position - (Vector2)transform.position).normalized;
    }

    protected override void FixedUpdate() {
        if (knockback.gettingKnockedBack) { return; }
        
        rb.MovePosition(rb.position + direction * (speed * Time.fixedDeltaTime));
    }
    private void FaceWaypoint(Transform waypoint)
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
