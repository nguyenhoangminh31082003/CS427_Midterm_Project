using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovement : MovementBase
{
    public Transform[] patrolPoints;
    private int targetPoint;

    protected override void Awake()
    {
        base.Awake();
        List<Transform> patrolPointsList = new List<Transform>();

        foreach (Transform sibling in transform.parent) {
            if (sibling != transform) {
                patrolPointsList.Add(sibling);
            }
        }

        patrolPoints = patrolPointsList.ToArray();
    }
    protected override void Start() {
        base.Start();
        targetPoint = 0;
    }

    public override void Roaming()
    {
        if (Vector2.Distance(transform.position, patrolPoints[targetPoint].position) < 0.1f)
        {
            targetPoint = (targetPoint + 1) % patrolPoints.Length;
        }
        MoveTo((patrolPoints[targetPoint].position - transform.position).normalized);
    }

    public override void Attacking()
    {
        
    }
}
